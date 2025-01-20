using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TaskManager
{
    public partial class AddTeamWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();

        public AddTeamWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersListBox.ItemsSource = taskManagerDBEntities.Users.ToList();
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {

            var selectedUsers = UsersListBox.SelectedItems.Cast<Users>().ToList();
            if (selectedUsers.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного участника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTeam = new Teams
            {
                NameTeam = TeamNameTextBox.Text
            };

            taskManagerDBEntities.Teams.Add(newTeam);

            taskManagerDBEntities.SaveChanges();

            DialogResult = true;
            Close();
        }
    }
}