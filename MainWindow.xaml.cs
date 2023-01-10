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

        private void btnProfessors_Click(object sender, RoutedEventArgs e)
        {
            var professorsWindow = new ShowProfessorsWindow();
            professorsWindow.ShowDialog();
            //professorsWindow.Show();
            //this.Close();
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            var studentsWindow = new ShowStudentsWindow();
            studentsWindow.ShowDialog();
            //this.Hide();
        }

        private void btnSchools_Click(object sender, RoutedEventArgs e)
        {
            var schoolsWindow = new ShowSchoolsWindow();
            schoolsWindow.ShowDialog();
            //this.Hide();
        }
        private void btnLanguages_Click(object sender, RoutedEventArgs e)
        {
            var languagesWindow = new ShowLanguagesWindow();
            languagesWindow.ShowDialog();
            //this.Hide();
        }
        private void btnLessons_Click(object sender, RoutedEventArgs e)
        {
            var lessonsWindow = new ShowLessonsWindow();
            lessonsWindow.ShowDialog();
            /*this.Hide();*/
        }
        private void btnAddresses_Click(object sender, RoutedEventArgs e)
        {
            var addressesWindow = new ShowAddressesWindow();
            addressesWindow.ShowDialog();
            //this.Hide();
        }
    }
}