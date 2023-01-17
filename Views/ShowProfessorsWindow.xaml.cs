using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SR46_2021_POP2022.Views
{
    /// <summary>
    /// Interaction logic for ShowProfessorsWindow.xaml
    /// </summary>
    /// 
    public partial class ShowProfessorsWindow : Window
    {
        private ProfessorService professorService = new ProfessorService();
        public enum State { ADMINISTRATION, DOWNLOADING };
        State state;
        public Professor SelectedProfessor = null;

        public ShowProfessorsWindow(State state = State.ADMINISTRATION, bool isRegistered = true)
        {
            InitializeComponent();
            RefreshDataGrid();
            this.state = state;

            if (state == State.DOWNLOADING)
            {
                miAddProfessor.Visibility = Visibility.Collapsed;
                miUpdateProfessor.Visibility = Visibility.Collapsed;
                miDeleteProfessor.Visibility = Visibility.Collapsed;
            }
            else if (!isRegistered)
            {
                miAddProfessor.Visibility = Visibility.Collapsed;
                miUpdateProfessor.Visibility = Visibility.Collapsed;
                miDeleteProfessor.Visibility = Visibility.Collapsed;
                miPickProfessor.Visibility = Visibility.Collapsed;
            }
            else if (state == State.ADMINISTRATION)
            {
                miPickProfessor.Visibility = Visibility.Hidden;
            }

            dgProfessors.ItemsSource = professorService.GetActiveProfessors();
            dgProfessors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    


    private void miPickProfessor_Click(object sender, RoutedEventArgs e)
        {
            SelectedProfessor = dgProfessors.SelectedItem as Professor;
            this.DialogResult = true;
            this.Close();
        }
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
                var professors = professorService.GetActiveProfessors();

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
                professorService.Delete(selectedUser.Id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<User> users = professorService.GetActiveProfessors().Select(p => p.User).ToList();
            dgProfessors.ItemsSource = users;
        }

        private void dgProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = txtSearch.Text;
                ProfessorService profService = new ProfessorService();
                List<Professor> filteredProfesors = profService.GetActiveProfessors()
                    .Where(prof => prof.User.FirstName.ToLower().Contains(searchTerm.ToLower())
                                 || prof.User.LastName.ToLower().Contains(searchTerm.ToLower())
                                 || prof.User.Email.ToLower().Contains(searchTerm.ToLower())
                             || prof.User.Address.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))


                    .ToList();

                dgProfessors.ItemsSource = filteredProfesors;
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