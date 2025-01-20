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
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        private Users _currentUser;
        public ProjectWindow(Users currentUser)
        {
            InitializeComponent();
            LoadTasks();
            _currentUser = currentUser;
        }

        private void LoadTasks()
        {
            DataGridProject.ItemsSource = taskManagerDBEntities.Project.ToList();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            AddProjectWindow addProjectWindow = new AddProjectWindow();
            if (addProjectWindow.ShowDialog() == true)
            {
                LoadTasks();
            }
        }

        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            var selectedProject = DataGridProject.SelectedItem as Project;

            if (selectedProject == null)
            {
                MessageBox.Show("Выберите задачу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            taskManagerDBEntities.Project.Remove(selectedProject);
            taskManagerDBEntities.SaveChanges();

            LoadTasks(); 
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.idRole == 1) 
            {
                WindowTasksAdmin windowTasksAdmin = new WindowTasksAdmin(_currentUser);
                windowTasksAdmin.Show();
            }
            else if (_currentUser.idRole == 2) 
            {
                WindowTasks windowTasks = new WindowTasks(_currentUser);
                windowTasks.Show();
            }
            this.Close();
        }
    }
}
