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
using static SR46_2021_POP2022.Views.AddEditStudentsWindow;

namespace SR46_2021_POP2022.Views
{
    /// <summary>
    /// Interaction logic for AddEditStudentsWindow.xaml
    /// </summary>
    public partial class AddEditStudentsWindow : Window
    {
        private Student student;
        private IStudentService studentService = new StudentService();
        private bool isAddMode;

    

        public AddEditStudentsWindow(Student student)
        {
            InitializeComponent();
            this.student = student.Clone() as Student;

            DataContext = this.student;

            isAddMode = false;
            txtJMBG.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }
        public AddEditStudentsWindow()
        {
            InitializeComponent();

            var user = new User
            {
                UserType = EUserType.STUDENT,
                IsActive = true
            };

            student = new Student
            {
                User = user
            };

            isAddMode = true;
            DataContext = student;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (student.User.IsValid)
            {
                if (isAddMode)
                {
                    studentService.Add(student);
                }
                else
                {
                    studentService.Update(student.User.Email, student);
                }

                DialogResult = true;
                Close();
            }
        
        //Data.Instance.StudentService.Add(newUser);

        //DialogResult = true;
        //Close();
    }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPickAddress_Click(object sender, RoutedEventArgs e)
        {
            ShowAddressesWindow aw = new ShowAddressesWindow(ShowAddressesWindow.State.DOWNLOADING);
            if (aw.ShowDialog() == true)
            {
                student.User.Address = aw.SelectedAddress;

            }
        }
    }
}
