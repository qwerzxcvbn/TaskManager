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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users()
            {
                FullName = Name.Text,
                Email = Email.Text,
                Login = LoginText.Text,
                Password = PasswordText.Password,
                idRole = 2
            };
            taskManagerDBEntities.Users.Add(users);
            taskManagerDBEntities.SaveChanges();
            MessageBox.Show("Пользователь зарегестрирован");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
