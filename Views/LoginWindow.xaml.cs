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
        public User _loggedInUser;
        public EUserType _loggedInUserType;
        private HomeWindow homeWindow = new HomeWindow();
     
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

                    this.Hide();

                    var homeWindow = new HomeWindow(_loggedInUser, _loggedInUserType);
                    homeWindow.lblLoggedInUser.Content = $"Logged in as {firstName} {lastName}";
                    homeWindow.UpdateUI();
                    homeWindow.Show();
                    var showStudentWindow = new ShowStudentsWindow(_loggedInUser);
                    var showProfessorWindow = new ShowProfessorsWindow(_loggedInUser);
                }
                else
                {
                    
                    MessageBox.Show("Invalid email or password. Please try again.");
                }

                reader.Close();
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
