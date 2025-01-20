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
    }
}
