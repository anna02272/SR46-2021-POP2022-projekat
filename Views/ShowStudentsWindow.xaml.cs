using SR46_2021_POP2022.Models;
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

namespace SR46_2021_POP2022.Views
{
    /// <summary>
    /// Interaction logic for ShowStudentsWindow.xaml
    /// </summary>
    public partial class ShowStudentsWindow : Window
    {
        public ShowStudentsWindow()
        {
            InitializeComponent();
            List<User> users = Data.Instance.StudentService.GetAll()
                .Select(p => p.User).ToList();
            dgStudents.ItemsSource = users;
        }

        private void miAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addEditStudentWindow = new AddEditStudentsWindow();

            var successeful = addEditStudentWindow.ShowDialog();

            if ((bool)successeful)
            {
                List<User> users = Data.Instance.StudentService.GetAll()
                .Select(p => p.User).ToList();
                dgStudents.ItemsSource = users;
            }
        }

        private void miUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (User)dgStudents.SelectedItem;
            if (selectedUser != null)
            {

            }
        }

        private void miDeleteStudent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}