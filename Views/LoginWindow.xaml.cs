using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();

           
        }
        private string _currentButton = "Lessons";
        public User _loggedInUser;
        public EUserType _loggedInUserType;
        private HomeWindow homeWindow = new HomeWindow();
        private Professor _loggedInProfessorId;
        private Student _loggedInStudentId;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            string email = txtEmail.Text;
            string password = txtPassword.Text;
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Users where Email='{email}' and Password='{password}'";
                SqlCommand command = new SqlCommand(commandText, conn);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int id = (int)reader["Id"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    EUserType userType = (EUserType)Enum.Parse(typeof(EUserType), reader["UserType"] as string);

                    _loggedInUser = new User
                    {
                        Id = id,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        UserType = userType
                    };

                    _loggedInUserType = userType;

                    reader.Close();

                    if (_loggedInUserType == EUserType.PROFESSOR)
                    {
                        string professorCommandText = $"select * from dbo.Professors where UserId = {id}";
                        SqlCommand professorCommand = new SqlCommand(professorCommandText, conn);

                        SqlDataReader professorReader = professorCommand.ExecuteReader();

                        if (professorReader.HasRows)
                        {
                            professorReader.Read();
                            int profId = (int)professorReader["Id"];

                            _loggedInProfessorId = new Professor
                            {
                                Id = profId

                            };


                            professorReader.Close();
                        }

                        this.Hide();
                        var homeWindow = new HomeWindow(_loggedInUser, _loggedInUserType, _loggedInProfessorId);
                        homeWindow.lblLoggedInUser.Content = $"Logged in as {firstName} {lastName}";
                        homeWindow.UpdateUI();
                        homeWindow.Show();
                       
                        var showProfessorWindow = new ShowProfessorsWindow(_loggedInUser, _loggedInUserType);
                        var showLessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInProfessorId, _currentButton, _loggedInUserType);
                       
                    }
                    else if (_loggedInUserType == EUserType.STUDENT)
                    {
                        string studentCommandText = $"select * from dbo.Students where UserId = {id}";
                        SqlCommand studentCommand = new SqlCommand(studentCommandText, conn);

                        SqlDataReader studentReader = studentCommand.ExecuteReader();

                        if (studentReader.HasRows)
                        {
                           studentReader.Read();
                            int studId = (int)studentReader["Id"];

                            _loggedInStudentId = new Student
                            {
                                Id = studId

                            };


                            studentReader.Close();
                        }

                        this.Hide();
                        var homeWindow = new HomeWindow(_loggedInUser, _loggedInUserType, _loggedInStudentId);
                        homeWindow.lblLoggedInUser.Content = $"Logged in as {firstName} {lastName}";
                        homeWindow.UpdateUI();
                        homeWindow.Show();
                        var showStudentWindow = new ShowStudentsWindow(_loggedInUser, _loggedInUserType);
                        var showProfessorWindow = new ShowProfessorsWindow(_loggedInUser, _loggedInUserType);
                        var showStudLessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInStudentId, _currentButton, _loggedInUserType);
                        var showLessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInUserType);

                    }

                    else if ( _loggedInUserType == EUserType.ADMINISTRATOR)
                    {
                        this.Hide();
                        var homeWindow2 = new HomeWindow(_loggedInUser, _loggedInUserType);
                        homeWindow2.lblLoggedInUser.Content = $"Logged in as {firstName} {lastName}";
                        homeWindow2.UpdateUI();
                        homeWindow2.Show();
                        var showStudentWindow = new ShowStudentsWindow(_loggedInUser, _loggedInUserType);
                        var showProfessorWindow = new ShowProfessorsWindow(_loggedInUser, _loggedInUserType);
                        var showLessonsWindow = new ShowLessonsWindow(_loggedInUser, _loggedInUserType);
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password. Please try again.");
                    }



                    }
                  


                    conn.Close();
                }
            }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
       

    }  
}
