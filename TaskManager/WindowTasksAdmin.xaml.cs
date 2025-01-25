using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для WindowTasksAdmin.xaml
    /// </summary>
    public partial class WindowTasksAdmin : Window
    {
        private TaskManagerDBEntities _context;
        private Users _currentUser;
        public WindowTasksAdmin(Users currentUser)
        {
            InitializeComponent();
            _context = new TaskManagerDBEntities();
            LoadTasks();
            _currentUser = currentUser;
        }

        private void LoadTasks()
        {
            DataGridTasks.ItemsSource = _context.Tasks.ToList();
        }

        private void DataGridTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = DataGridTasks.SelectedItem as Tasks;

            if (selectedTask != null)
            {
                AddTaskWindow addTaskWindow = new AddTaskWindow(selectedTask, _context);
                if (addTaskWindow.ShowDialog() == true)
                {
                    LoadTasks();
                }
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow(null, _context);
            if (addTaskWindow.ShowDialog() == true)
            {
                LoadTasks();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = DataGridTasks.SelectedItem as Tasks;

            if (selectedTask == null)
            {
                MessageBox.Show("Выберите задачу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.Tasks.Remove(selectedTask);
            _context.SaveChanges();

            LoadTasks();

            MessageBox.Show("Задача успешно удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectWindow projectWindow = new ProjectWindow(_currentUser);
            projectWindow.Show();
            this.Close();
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow teamWindow = new TeamWindow(_currentUser);
            teamWindow.Show();
            this.Close();
        }

        private void ListUser_Click(object sender, RoutedEventArgs e)
        {
            ListUserWindow listUserWindow = new ListUserWindow(_currentUser);
            listUserWindow.Show();
            this.Close();
        }

        private void TXTButton_Click(object sender, RoutedEventArgs e)
        {
            var tasks = DataGridTasks.ItemsSource as IEnumerable<Tasks>;
            if (tasks == null)
            {
                MessageBox.Show("Нет данных для экспорта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                DefaultExt = ".txt",
                FileName = "tasks_export.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine($"Название: {task.NameTask}");
                        writer.WriteLine($"Описание: {task.Description}");
                        writer.WriteLine($"Дата: {task.Data}");
                        writer.WriteLine($"Статус: {task.Status.NameStatus}");
                        writer.WriteLine($"Проект: {task.Project.NameProject}");
                        writer.WriteLine($"Исполнитель: {task.Users.FullName}");
                        writer.WriteLine($"Команда: {task.Teams.NameTeam}");
                        writer.WriteLine(new string('-', 50));
                    }
                }

                MessageBox.Show("Данные успешно экспортированы в TXT.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void JSONButton_Click(object sender, RoutedEventArgs e)
        {
            var tasks = DataGridTasks.ItemsSource as IEnumerable<Tasks>;
            if (tasks == null)
            {
                MessageBox.Show("Нет данных для экспорта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json",
                FileName = "tasks_export.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(saveFileDialog.FileName, json);

                MessageBox.Show("Данные успешно экспортированы в JSON.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
