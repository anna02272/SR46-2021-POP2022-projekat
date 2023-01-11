using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
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
       

        public enum State { ADMINISTRATION, DOWNLOADING };
        State state;
        public Student SelectedStudent = null;

        public ShowStudentsWindow(State state = State.ADMINISTRATION)
        {
            InitializeComponent();
            this.state = state;

            if (state == State.DOWNLOADING)
            {
                miAddStudent.Visibility = Visibility.Collapsed;
                miUpdateStudent.Visibility = Visibility.Collapsed;
                miDeleteStudent.Visibility = Visibility.Collapsed;
            }
            else
            {
                miPickStudent.Visibility = Visibility.Hidden;
            }

            dgStudents.ItemsSource = studentService.GetActiveStudents();

            dgStudents.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        private void miPickStudent_Click(object sender, RoutedEventArgs e)
        {
            SelectedStudent = dgStudents.SelectedItem as Student;
            this.DialogResult = true;
            this.Close();
        }
        public ShowStudentsWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
           

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
                var students = studentService.GetActiveStudents();
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
                studentService.Delete(selectedUser.Id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<User> users = studentService.GetActiveStudents().Select(s => s.User).ToList();
            dgStudents.ItemsSource = users;

           
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