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
    /// Логика взаимодействия для NewPasswordWindow.xaml
    /// </summary>
    public partial class NewPasswordWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        private string _email;
        public NewPasswordWindow(string email)
        {         
            InitializeComponent();
            _email = email;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Введите новый пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = taskManagerDBEntities.Users.FirstOrDefault(u => u.Email == _email);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user.Password = newPassword;
            taskManagerDBEntities.SaveChanges();

            MessageBox.Show("Пароль успешно изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
