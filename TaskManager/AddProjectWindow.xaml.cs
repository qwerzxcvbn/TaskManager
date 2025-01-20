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
    /// Логика взаимодействия для AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        TaskManagerDBEntities taskManagerDBEntities = new TaskManagerDBEntities();
        public AddProjectWindow()
        {
            InitializeComponent();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            var newProject = new Project
            {
                NameProject = TitleTextBox.Text,
                Description = DescriptionTextBox.Text                
            };

            taskManagerDBEntities.Project.Add(newProject);
            taskManagerDBEntities.SaveChanges();

            DialogResult = true;
            Close();
        }
    }
}
