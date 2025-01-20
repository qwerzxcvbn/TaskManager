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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        private Users _currentUser;
        public AddUserWindow(Users currentUser)
        {
            InitializeComponent();
            LoadData();
            _currentUser = currentUser;
        }

        private void LoadData()
        {
            RoleComboBox.ItemsSource = taskManagerDBEntities.Roles.ToList();
        }

        private void AddUser_Button_Click(object sender, RoutedEventArgs e)
        {
            
            Users users = new Users()
            {
                FullName = Name.Text,
                Email = Email.Text,
                Login = LoginText.Text,
                Password = PasswordText.Password,
                idRole = (RoleComboBox.SelectedItem as Roles)?.idRole
            };

            taskManagerDBEntities.Users.Add(users);
            taskManagerDBEntities.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ListUserWindow listUserWindow = new ListUserWindow(_currentUser);
            listUserWindow.Show();
            this.Close();
        }
    }
}
