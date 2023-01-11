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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private User user;
        private IUserService userService = new UserService();
        private bool isAddMode;

        public LoginWindow(User user)
        {
            InitializeComponent();
            this.user = user.Clone() as User;

            DataContext = user;

            isAddMode = false;
        }


        public LoginWindow()
        {
            InitializeComponent();

            user = new User
            {

                IsActive = true
            };



            isAddMode = true;
            DataContext = user;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                User user = new User();
                if (user.Login(email, password))
                {
                    // Login successful
                   
                    HomeWindow homeWindow = new HomeWindow();
                   homeWindow.Show();
                    Close();
                }
                else
                {
                    // Login failed
                    // Show an error message
                    MessageBox.Show("Invalid email or password.");
                }
                Close();
            }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


    }  
}
