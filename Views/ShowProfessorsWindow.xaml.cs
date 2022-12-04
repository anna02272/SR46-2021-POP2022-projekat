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
    /// Interaction logic for ShowProfessorsWindow.xaml
    /// </summary>
    /// 
    public partial class ShowProfessorsWindow : Window
    {
        private ProfessorService professorService = new ProfessorService();

        public ShowProfessorsWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void miAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            var addEditProfessorWindow = new AddEditProfessorsWindow();

            var successeful = addEditProfessorWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgProfessors.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var professors = professorService.GetAll();

                var addEditProfessorWindow = new AddEditProfessorsWindow(professors[selectedIndex]);

                var successeful = addEditProfessorWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgProfessors.SelectedItem as User;

            if (selectedUser != null)
            {
                professorService.Delete(selectedUser.Email);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<User> users = professorService.GetAll().Select(p => p.User).ToList();
            dgProfessors.ItemsSource = users;
        }

        private void dgProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
//    public partial class ShowProfessorsWindow : Window
//    {
//        public ShowProfessorsWindow()
//        {
//            InitializeComponent();
//            List<User> users = Data.Instance.ProfessorService.GetAll()
//                .Select(p => p.User).ToList();
//            dgProfessors.ItemsSource = users;
//        }

//        private void miAddProfessor_Click(object sender, RoutedEventArgs e)
//        {
//            var addEditProfessorWindow = new AddEditProfessorsWindow();

//            var successeful = addEditProfessorWindow.ShowDialog();

//            if ((bool)successeful)
//            {
//                List<User> users = Data.Instance.ProfessorService.GetAll()
//                .Select(p => p.User).ToList();
//                dgProfessors.ItemsSource = users;
//            }
//        }

//        private void miUpdateProfessor_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedUser = (User)dgProfessors.SelectedItem;
//            if (selectedUser != null)
//            {

//            }
//        }

//        private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
//        {

//        }

//        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            dgProfessors.ItemsSource = Data.Instance.ProfessorService.Search(txtSearch.Text)
//                .Select(p => p.User).ToList();

//        }
//    }
//}