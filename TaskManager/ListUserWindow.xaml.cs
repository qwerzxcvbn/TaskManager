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
    /// Логика взаимодействия для ListUserWindow.xaml
    /// </summary>
    public partial class ListUserWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        private Users _currentUser;
        public ListUserWindow(Users currentUser)
        {
            InitializeComponent();
            LoadTasks();
            _currentUser = currentUser;
        }
        private void LoadTasks()
        {
            DataGridUser.ItemsSource = taskManagerDBEntities.Users.ToList();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(_currentUser);
            if (addUserWindow.ShowDialog() == true)
            {
                LoadTasks();
            }
        }

        private void RedactUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DataGridUser.SelectedItem as Users;

            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RedactUserWindow redactUserWindow = new RedactUserWindow(selectedUser, taskManagerDBEntities);
            if (redactUserWindow.ShowDialog() == true)
            {
                LoadTasks();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DataGridUser.SelectedItem as Users;

            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            taskManagerDBEntities.Users.Remove(selectedUser);
            taskManagerDBEntities.SaveChanges();

            LoadTasks();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            WindowTasksAdmin windowTasksAdmin = new WindowTasksAdmin(_currentUser);
            windowTasksAdmin.Show();
            this.Close();
        }
    }
}
