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
    /// Interaction logic for NotRegisteredHomeWindow.xaml
    /// </summary>
    public partial class NotRegisteredHomeWindow : Window
    {

        public NotRegisteredHomeWindow()
        {

            InitializeComponent();

        }

        private void btnProfessors_Click(object sender, RoutedEventArgs e)
        {
            var professorsWindow = new ShowProfessorsWindow(ShowProfessorsWindow.State.ADMINISTRATION, false);
            professorsWindow.ShowDialog();
        }



        private void btnSchools_Click(object sender, RoutedEventArgs e)
        {
            var schoolsWindow = new ShowSchoolsWindow( false);
            schoolsWindow.ShowDialog();

        }
      
        }
    }
