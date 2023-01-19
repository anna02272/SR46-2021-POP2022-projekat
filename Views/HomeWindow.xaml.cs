using SR46_2021_POP2022.Models;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Web;
using SR46_2021_POP2022.Services;
using System.Collections.Generic;
using System.Linq;

namespace SR46_2021_POP2022.Views
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {

            InitializeComponent();

        }

        private void btnProfessors_Click(object sender, RoutedEventArgs e)
        {
            var professorsWindow = new ShowProfessorsWindow(_loggedInUser, _loggedInUserType);
            professorsWindow.ShowDialog();
            //professorsWindow.Show();
            //this.Close();
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            var studentsWindow = new ShowStudentsWindow(_loggedInUser, _loggedInUserType);
            studentsWindow.ShowDialog();
            //this.Hide();
        }

        private void btnSchools_Click(object sender, RoutedEventArgs e)
        {
            var schoolsWindow = new ShowSchoolsWindow( _loggedInUserType);
            schoolsWindow.ShowDialog();

        }
        private void btnLanguages_Click(object sender, RoutedEventArgs e)
        {
            var languagesWindow = new ShowLanguagesWindow();
            languagesWindow.ShowDialog();

        }

        private string _currentButton = "Lessons";

        private void btnLessons_Click(object sender, RoutedEventArgs e)
        {
            _currentButton = "Lessons";
            var lessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInProfessorId, _currentButton, _loggedInUserType);
            lessonsWindow.ShowDialog();
        }

        private void btnReservedLessons_Click(object sender, RoutedEventArgs e)
        {
            _currentButton = "ReservedLessons";
            var reservedLessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInStudentId, _currentButton, _loggedInUserType);
            reservedLessonsWindow.ShowDialog();
        }





        private void btnAddresses_Click(object sender, RoutedEventArgs e)
        {
            var addressesWindow = new ShowAddressesWindow();
            addressesWindow.ShowDialog();

        }
        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new ShowUsersWindow();
            usersWindow.ShowDialog();

        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _loggedInUser = null;
            _loggedInUserType = default(EUserType);

            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private Professor _loggedInProfessorId;
        private Student _loggedInStudentId;
        private User _loggedInUser;
        private EUserType _loggedInUserType;
        public HomeWindow(EUserType loggedInUserType)
        {
            _loggedInUserType = loggedInUserType;
            InitializeComponent();
        }


        private StudentService studentService = new StudentService();
        private ShowStudentsWindow stWindow = new ShowStudentsWindow();
        private ShowProfessorsWindow prWindow = new ShowProfessorsWindow();

        public HomeWindow(User loggedInUser, EUserType loggedInUserType, Student loggedInStudentId)
        {
            InitializeComponent();
            _loggedInStudentId = loggedInStudentId;
            _loggedInUser = loggedInUser;
            _loggedInUserType = loggedInUserType;
            UpdateUI();
        }
        public HomeWindow(User loggedInUser, EUserType loggedInUserType, Professor loggedInProfessorId)
        {
            InitializeComponent();
            _loggedInProfessorId = loggedInProfessorId;
            _loggedInUser = loggedInUser;
            _loggedInUserType = loggedInUserType;
             UpdateUI();
        }
        public HomeWindow(User loggedInUser, EUserType loggedInUserType)
        {
            InitializeComponent();
           
            _loggedInUser = loggedInUser;
            _loggedInUserType = loggedInUserType;
            UpdateUI();
        }

        public void UpdateUI()
        {
          
            if (_loggedInUser == null || _loggedInUserType == default(EUserType))
            {
                btnReservedLessons.Visibility = Visibility.Collapsed;   
            }
            else if (_loggedInUserType == EUserType.STUDENT)
            {
             
                btnUsers.Visibility = Visibility.Collapsed;
                //btnStudents.Visibility = Visibility.Collapsed;

                btnLanguages.Visibility = Visibility.Collapsed;

                btnAddresses.Visibility = Visibility.Collapsed;
            }
            else if (_loggedInUserType == EUserType.PROFESSOR)
            {
                btnUsers.Visibility = Visibility.Collapsed;
                btnStudents.Visibility = Visibility.Collapsed;
                //btnProfessors.Visibility = Visibility.Collapsed;
                btnSchools.Visibility = Visibility.Collapsed;
                btnLanguages.Visibility = Visibility.Collapsed;

                btnAddresses.Visibility = Visibility.Collapsed;

                btnReservedLessons.Visibility = Visibility.Collapsed;
            }
        }

    
    }

}