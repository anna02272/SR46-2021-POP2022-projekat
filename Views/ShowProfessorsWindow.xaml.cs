﻿using SR46_2021_POP2022.Models;
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

        private User _loggedInUser;
        private EUserType _loggedInUserType;

        public ShowProfessorsWindow(EUserType loggedInUserType)
        {
            _loggedInUserType = loggedInUserType;
            InitializeComponent();
        }

        public ShowProfessorsWindow(User loggedInUser, EUserType loggedInUserType)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _loggedInUserType = loggedInUserType;
            RefreshDataGrid();

            if (_loggedInUserType == EUserType.STUDENT)
            {
                miAddProfessor.Visibility = Visibility.Collapsed;
                miUpdateProfessor.Visibility = Visibility.Collapsed;
                miDeleteProfessor.Visibility = Visibility.Collapsed;
                miPickProfessor.Visibility = Visibility.Collapsed;
            }
            else if (_loggedInUserType == EUserType.PROFESSOR)
            {
                miAddProfessor.Visibility = Visibility.Collapsed;
                miDeleteProfessor.Visibility = Visibility.Collapsed;
                miPickProfessor.Visibility = Visibility.Collapsed;
            }
            else
            {
                miPickProfessor.Visibility = Visibility.Collapsed;
            }
        }

        
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
            if (dgProfessors.SelectedIndex >= 0)
            {
                var selected_id = (dgProfessors.SelectedItem as User).Id;
                var professors = professorService.GetActiveProfessors().Where(s => s.User.Id == selected_id);
                if (_loggedInUserType == EUserType.PROFESSOR)
                {
                    professors = professors.Where(s => s.User.Id == _loggedInUser.Id);
                }
                var selectedProfessor = professors.FirstOrDefault();
                var addEditProfessorsWindow = new AddEditProfessorsWindow(selectedProfessor);
                var succesful = addEditProfessorsWindow.ShowDialog();
                if ((bool)succesful)
                {
                    RefreshDataGrid();
                }
            }
        }
            //private void miUpdateProfessor_Click(object sender, RoutedEventArgs e)
            //{
            //    var selectedIndex = dgProfessors.SelectedIndex;

            //    if (selectedIndex >= 0)
            //    {
            //        var professors = professorService.GetActiveProfessors();

            //        var addEditProfessorWindow = new AddEditProfessorsWindow(professors[selectedIndex]);

            //        var successeful = addEditProfessorWindow.ShowDialog();

            //        if ((bool)successeful)
            //        {
            //            RefreshDataGrid();
            //        }
            //    }
            //}

            private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgProfessors.SelectedItem as User;

            if (selectedUser != null)
            {
                professorService.Delete(selectedUser.Id);
                RefreshDataGrid();
            }
        }

        //private void RefreshDataGrid()
        //{
        //    List<User> users = professorService.GetActiveProfessors().Select(p => p.User).ToList();
        //    dgProfessors.ItemsSource = users;
        //}
        private void RefreshDataGrid()
        {
            if (_loggedInUser == null)
            {
                List<User> users = professorService.GetActiveProfessors().Select(s => s.User).ToList();
                dgProfessors.ItemsSource = users;
            }
            else if (_loggedInUser.UserType == EUserType.PROFESSOR)
            {
                List<User> professors = professorService.GetActiveProfessors()
                                                    .Where(s => s.User.Id == _loggedInUser.Id)
                                                    .Select(s => s.User)
                                                    .ToList();
                dgProfessors.ItemsSource = professors;
            }
            else
            {
                List<User> users = professorService.GetActiveProfessors().Select(s => s.User).ToList();
                dgProfessors.ItemsSource = users;
            }
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
                             || prof.User.Address.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase)
                               || prof.School.Name.ToLower().Contains(searchTerm.ToLower()))

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