using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для RedactUserWindow.xaml
    /// </summary>
    public partial class RedactUserWindow : Window
    {
        private Users _userToEdit;
        private TaskManagerDBEntities _context;

        public RedactUserWindow(Users userToEdit, TaskManagerDBEntities context)
        {
            InitializeComponent();
            _context = context;
            _userToEdit = userToEdit;
            LoadData();
            LoadUserData();
        }

        private void LoadData()
        {
            RoleComboBox.ItemsSource = _context.Roles.ToList();
            RoleComboBox.DisplayMemberPath = "NameRole";
            RoleComboBox.SelectedValuePath = "idRole";
        }

        private void LoadUserData()
        {
            if (_userToEdit != null)
            {
                Name.Text = _userToEdit.FullName;
                Email.Text = _userToEdit.Email;
                LoginText.Text = _userToEdit.Login;
                PasswordText.Password = _userToEdit.Password;
                RoleComboBox.SelectedValue = _userToEdit.idRole;
            }
        }

        private void RedactUser_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var existingUser = _context.Users.Local.FirstOrDefault(u => u.idUser == _userToEdit.idUser);
                if (existingUser != null)
                {
                    _context.Entry(existingUser).State = EntityState.Detached;
                }
                _context.Users.Attach(_userToEdit);

                _userToEdit.FullName = Name.Text;
                _userToEdit.Email = Email.Text;
                _userToEdit.Login = LoginText.Text;
                _userToEdit.Password = PasswordText.Password;
                _userToEdit.idRole = (RoleComboBox.SelectedItem as Roles)?.idRole;

                _context.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {           
            DialogResult = false;
            Close();
        }
    }
}
