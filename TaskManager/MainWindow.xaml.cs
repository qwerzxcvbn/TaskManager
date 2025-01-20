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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Recovery_Button_Click(object sender, RoutedEventArgs e)
        {
            RecoveryWindow recoveryWindow = new RecoveryWindow();
            recoveryWindow.Show();
            this.Close();
        }

        private void Registration_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private void Vxod_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginfo = taskManagerDBEntities.Users.Where(p => p.Login == Login.Text && p.Password == Password.Password).Single();

                if (loginfo.idRole == 1)
                {
                    WindowTasksAdmin windowTasksAdmin = new WindowTasksAdmin(loginfo);
                    windowTasksAdmin.Show();
                    this.Close();
                }
                else
                {
                    WindowTasks windowTasks = new WindowTasks(loginfo);
                    windowTasks.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
