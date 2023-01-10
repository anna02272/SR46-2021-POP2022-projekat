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
    /// Interaction logic for AddEditProfessorsWindow.xaml
    /// </summary>
    
    public partial class AddEditProfessorsWindow : Window
    {
        private Professor professor;
        private IProfessorService professorService = new ProfessorService();
        private bool isAddMode;

        public AddEditProfessorsWindow(Professor professor)
        {
            InitializeComponent();
            this.professor = professor.Clone() as Professor;

            DataContext = this.professor;

            isAddMode = false;
            txtJMBG.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }

        public AddEditProfessorsWindow()
        {
            InitializeComponent();

            var user = new User
            {
                UserType = EUserType.PROFESSOR,
                IsActive = true
            };

            professor = new Professor
            {
                User = user
            };

            isAddMode = true;
            DataContext = professor;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (professor.User.IsValid)
            {
                if (isAddMode)
                {
                    professorService.Add(professor);
                }
                else
                {
                    professorService.Update(professor.Id, professor);
                }

                DialogResult = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void btnPickAddress_Click(object sender, RoutedEventArgs e)
        {
            ShowAddressesWindow aw = new ShowAddressesWindow(ShowAddressesWindow.State.DOWNLOADING);
            if (aw.ShowDialog() == true)
            {
                professor.User.Address = aw.SelectedAddress;

            }
        }
    }
}
//    public partial class AddEditProfessorsWindow : Window
//    {
//        private User newUser;
//        private Professor professor;

//        public AddEditProfessorsWindow()
//        {
//            InitializeComponent();
//            //User user = new User
//            //{

//            //}
//            //proffesor = new Professor
//            //{

//            //}
//            //newUser = new User
//            //{
//            //    UserType = EUserType.PROFESSOR 

//            //};

//            //DataContext = newUser;
//        }

//        private void btnSave_Click(object sender, RoutedEventArgs e)
//        {
//            Data.Instance.ProfessorService.Add(newUser);

//            DialogResult = true;
//            Close();
//        }

//       
//    }
//}
