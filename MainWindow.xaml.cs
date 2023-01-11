using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SR46_2021_POP2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
            public MainWindow()
            {
                InitializeComponent();
            }

            private void btnStart_Click(object sender, RoutedEventArgs e)
            {
                var homeWindow = new HomeWindow();
                homeWindow.Show();
               
            }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
           
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var addEditStudentsWindow = new AddEditStudentsWindow();
            addEditStudentsWindow.Show();
           
        }
    }
    }
