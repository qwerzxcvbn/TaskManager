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
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private Tasks _taskToEdit;
        private TaskManagerDBEntities _context;

        public AddTaskWindow(Tasks taskToEdit, TaskManagerDBEntities context)
        {
            InitializeComponent();
            _context = context;
            _taskToEdit = taskToEdit;
            LoadData();
            LoadTaskData();
        }

        private void LoadTaskData()
        {
            if (_taskToEdit != null)
            {
                TitleTextBox.Text = _taskToEdit.NameTask;
                DescriptionTextBox.Text = _taskToEdit.Description;
                DatePicker.SelectedDate = _taskToEdit.Data;
                StatusComboBox.SelectedValue = _taskToEdit.idStatus;
                ProjectComboBox.SelectedValue = _taskToEdit.idProject;
                UserComboBox.SelectedValue = _taskToEdit.idUser;
                TeamComboBox.SelectedValue= _taskToEdit.idTeam;
            }
        }

        private void LoadData()
        {
            StatusComboBox.ItemsSource = _context.Status.ToList();
            ProjectComboBox.ItemsSource = _context.Project.ToList();
            UserComboBox.ItemsSource = _context.Users.ToList();
            TeamComboBox.ItemsSource= _context.Teams.ToList();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (_taskToEdit == null)
            {
                var newTask = new Tasks
                {
                    NameTask = TitleTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Data = DatePicker.SelectedDate.Value,
                    idStatus = (StatusComboBox.SelectedItem as Status)?.idStatus,
                    idProject = (ProjectComboBox.SelectedItem as Project)?.idProject,
                    idUser = (UserComboBox.SelectedItem as Users)?.idUser,
                    idTeam = (TeamComboBox.SelectedItem as Teams)?.idTeam
                };

                _context.Tasks.Add(newTask);
            }
            else
            {
                _taskToEdit.NameTask = TitleTextBox.Text;
                _taskToEdit.Description = DescriptionTextBox.Text;
                _taskToEdit.Data = DatePicker.SelectedDate.Value;
                _taskToEdit.idStatus = (StatusComboBox.SelectedItem as Status)?.idStatus;
                _taskToEdit.idProject = (ProjectComboBox.SelectedItem as Project)?.idProject;
                _taskToEdit.idUser = (UserComboBox.SelectedItem as Users)?.idUser;
                _taskToEdit.idTeam = (TeamComboBox.SelectedItem as Teams)?.idTeam;
            }

            _context.SaveChanges();
            DialogResult = true;
            Close();
        }
    }
}
