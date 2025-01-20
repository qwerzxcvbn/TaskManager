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
    /// Логика взаимодействия для RecoveryWindow.xaml
    /// </summary>
    public partial class RecoveryWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        public RecoveryWindow()
        {
            InitializeComponent();
        }  
        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RecoveryPass_Button_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            
            var user = taskManagerDBEntities.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                MessageBox.Show("Пользователь с указанной почтой не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 
            NewPasswordWindow newPasswordWindow = new NewPasswordWindow(email);
            newPasswordWindow.Show();
            this.Close();
        }
    }
}
