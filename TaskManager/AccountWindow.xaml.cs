using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        private TaskManagerDBEntities _context;
        private Users _currentUser;

        public AccountWindow(Users currentUser, TaskManagerDBEntities context)
        {
            InitializeComponent();
            _context = context;
            _currentUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.idUser == currentUser.idUser); 
            LoadUserData();
        }

        private void LoadUserData()
        {
            Name.Text = _currentUser.FullName;
            Email.Text = _currentUser.Email;
            LoginText.Text = _currentUser.Login;
            PasswordText.Text = _currentUser.Password;
        }

        private void SaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentUser.FullName = Name.Text;
                _currentUser.Email = Email.Text;
                _currentUser.Login = LoginText.Text;
                _currentUser.Password = PasswordText.Text;

                var existingEntity = _context.Users.Local.FirstOrDefault(u => u.idUser == _currentUser.idUser);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(_currentUser);
                }
                else
                {
                    _context.Users.Attach(_currentUser);
                    _context.Entry(_currentUser).State = System.Data.Entity.EntityState.Modified;
                }

                _context.SaveChanges();
                MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WindowTasks windowTasks = new WindowTasks(_currentUser);
            windowTasks.Show();
            this.Close();
        }
    }
}