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
    /// Interaction logic for ShowSchoolsWindow.xaml
    /// </summary>
    public partial class ShowSchoolsWindow : Window
    {
        private SchoolService schoolService = new SchoolService();
        public enum State { ADMINISTRATION, DOWNLOADING };
        State state;
        public School SelectedSchool = null;

        private bool isRegistered;

        public ShowSchoolsWindow(State state = State.ADMINISTRATION, bool isRegistered = true)
        {
            InitializeComponent();
            RefreshDataGrid();
            this.state = state;

            if (state == State.DOWNLOADING)
            {
                miAddSchool.Visibility = Visibility.Collapsed;
                miUpdateSchool.Visibility = Visibility.Collapsed;
                miDeleteSchool.Visibility = Visibility.Collapsed;
            }
            else if (!isRegistered)
            {
                miAddSchool.Visibility = Visibility.Collapsed;
                miUpdateSchool.Visibility = Visibility.Collapsed;
                miDeleteSchool.Visibility = Visibility.Collapsed;
                miPickSchool.Visibility = Visibility.Collapsed;
            }
            else if (state == State.ADMINISTRATION)
            {
                miPickSchool.Visibility = Visibility.Hidden;
            }


            dgSchools.ItemsSource = schoolService.GetActiveSchools();
            dgSchools.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }



        private void miPickSchool_Click(object sender, RoutedEventArgs e)
        {
            SelectedSchool = dgSchools.SelectedItem as School;
            this.DialogResult = true;
            this.Close();
        }

        private void miAddSchool_Click(object sender, RoutedEventArgs e)
        {
            var addEditSchoolWindow = new AddEditSchoolsWindow();
       
            var successeful = addEditSchoolWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateSchool_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgSchools.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var schools = schoolService.GetActiveSchools();

                var addEditSchoolWindow = new AddEditSchoolsWindow(schools[selectedIndex]);

                var successeful = addEditSchoolWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteSchool_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchool = dgSchools.SelectedItem as School;

            if (selectedSchool != null)
            {
                schoolService.Delete(selectedSchool.Id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<School> schools = schoolService.GetActiveSchools().Select(p => p).ToList();
            dgSchools.ItemsSource = schools;
        }

        private void dgSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        //private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        string searchTerm = txtSearch.Text;
        //        SchoolService schoolService = new SchoolService();
        //        List<School> filteredSchool = schoolService.GetActiveSchools()
        //            .Where(school =>
        //            //school.Name.ToLower().Contains(searchTerm.ToLower())
        //                          //||
        //                          school.Address.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
        //                     //|| school.Language.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))


        //            .ToList();

        //        dgSchools.ItemsSource = filteredSchool;
        //    }
        //}

        List<School> filteredSchoolByAddress = new List<School>();
        List<School> filteredSchoolByLanguage = new List<School>();
        List<School> filteredSchoolByName = new List<School>();

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = txtSearch.Text;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    SchoolService schoolService = new SchoolService();
                    filteredSchoolByAddress = schoolService.GetActiveSchools();
                    dgSchools.ItemsSource = filteredSchoolByAddress;
                    return;
                }
                filteredSchoolByAddress = schoolService.GetActiveSchools()
                    .Where(school => school.Address.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dgSchools.ItemsSource = filteredSchoolByAddress;
            }
        }

        private void txtSearch2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = txtSearch2.Text;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    dgSchools.ItemsSource = filteredSchoolByLanguage;
                    return;
                }
                filteredSchoolByLanguage = schoolService.GetActiveSchools()
                    .Where(school => school.Language.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                dgSchools.ItemsSource = filteredSchoolByLanguage;
            }
        }

        private void txtSearch3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = txtSearch3.Text;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    dgSchools.ItemsSource = filteredSchoolByName;
                    return;
                }
                filteredSchoolByName = schoolService.GetActiveSchools()
                    .Where(school => school.Name.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();

                dgSchools.ItemsSource = filteredSchoolByName;
            }
        }



    }
}
