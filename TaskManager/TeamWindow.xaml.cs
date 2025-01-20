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
    /// Логика взаимодействия для TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        private Users _currentUser;
        public TeamWindow(Users currentUser)
        {
            InitializeComponent();
            LoadTasks();
            _currentUser = currentUser;
        }

        private void LoadTasks()
        {
            DataGridTeam.ItemsSource = taskManagerDBEntities.Teams.ToList();
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

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeamWindow addTeamWindow = new AddTeamWindow();
            if (addTeamWindow.ShowDialog() == true)
            {
                LoadTasks();
            }
        }

        private void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeam = DataGridTeam.SelectedItem as Teams;

            if (selectedTeam == null)
            {
                MessageBox.Show("Выберите команду для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            taskManagerDBEntities.Teams.Remove(selectedTeam);
            taskManagerDBEntities.SaveChanges();

            LoadTasks();
        }
    }
}
