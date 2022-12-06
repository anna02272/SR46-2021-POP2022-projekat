using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Services;
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
        private StudentService studentService = new StudentService();
        public ShowStudentsWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
            //InitializeComponent();
            //List<User> users = Data.Instance.StudentService.GetAll()
            //    .Select(p => p.User).ToList();
            //dgStudents.ItemsSource = users;
        }

        private void miAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addEditStudentWindow = new AddEditStudentsWindow();

            var successeful = addEditStudentWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
                //List<User> users = Data.Instance.StudentService.GetAll()
                //.Select(p => p.User).ToList();
                //dgStudents.ItemsSource = users;
            }
        }


        private void miUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgStudents.SelectedIndex;
            if (selectedIndex >= 0)
            {
                var students = studentService.GetAll();
                var addEditStudentsWindow = new AddEditStudentsWindow(students[selectedIndex]);
                var succesful = addEditStudentsWindow.ShowDialog();

                if ((bool)succesful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgStudents.SelectedItem as User;

            if (selectedUser != null)
            {
                studentService.Delete(selectedUser.Email);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<User> users = studentService.GetAll().Select(s => s.User).ToList();
            dgStudents.ItemsSource = users;

            //List<User> users = Data.Instance.StudentService.GetAll()
            //    .Select(p => p.User).ToList();
            //dgStudents.ItemsSource = users;
        }
        private void dgStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}