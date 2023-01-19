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
    /// Interaction logic for ShowLessonsWindow.xaml
    /// </summary>
    public partial class ShowLessonsWindow : Window
    {
        private LessonService lessonService = new LessonService();
        private AddEditLessonsWindow addEditLessonWindow;

        private string _currentButton = "Lessons";
        private Student _loggedInStudentId;
        private Professor _loggedInProfessorId;
        private User _loggedInUser;
        private EUserType _loggedInUserType;
       

        public ShowLessonsWindow(EUserType loggedInUserType)
        {
            _loggedInUserType = loggedInUserType;
            InitializeComponent();

        }

        public ShowLessonsWindow(User loggedInUser, Professor loggedInProfessorId, string currentButton, EUserType loggedInUserType)
        {
            InitializeComponent();
            _loggedInProfessorId = loggedInProfessorId;
            _loggedInUser = loggedInUser;
            _currentButton = currentButton;
            _loggedInUserType = loggedInUserType;
            RefreshDataGrid();

            if (_loggedInUserType == EUserType.STUDENT)
            {
                miAddLesson.Visibility = Visibility.Collapsed;
                miDeleteLesson.Visibility = Visibility.Collapsed;
               
            }
            else
            {
               
            }
        }


        public ShowLessonsWindow(User loggedInUser, Student loggedInStudentId, string currentButton, EUserType loggedInUserType)
        {
            InitializeComponent();
            _loggedInStudentId = loggedInStudentId;
            _loggedInUser = loggedInUser;
            _currentButton = currentButton;
            _loggedInUserType = loggedInUserType;
            RefreshDataGrid();

            if (_loggedInUserType == EUserType.STUDENT)
            {
                miAddLesson.Visibility = Visibility.Collapsed;
                miDeleteLesson.Visibility = Visibility.Collapsed;

            }
            else
            {

            }


        }
        public ShowLessonsWindow(User loggedInUser, EUserType loggedInUserType)
        {
            InitializeComponent();
          
            _loggedInUser = loggedInUser;
            _loggedInUserType = loggedInUserType;

            RefreshDataGrid();

            if (_loggedInUserType == EUserType.STUDENT)
            {
                miAddLesson.Visibility = Visibility.Collapsed;
                miDeleteLesson.Visibility = Visibility.Collapsed;

            }
            else
            {

            }


        }
        public ShowLessonsWindow()
        {

            InitializeComponent();
            RefreshDataGrid();
        }

    
        private void miAddLesson_Click(object sender, RoutedEventArgs e)
        {
            var addEditLessonWindow = new AddEditLessonsWindow();

            var successeful = addEditLessonWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateLesson_Click(object sender, RoutedEventArgs e)
        {
            if (dgLessons.SelectedIndex >= 0)
            {
                var selected_id = (dgLessons.SelectedItem as Lesson).Id;
                var lessons = lessonService.GetAvailableLessons().Where(s => s.Id == selected_id);
                if (_loggedInUserType == EUserType.PROFESSOR)
                {
                    lessons = lessons.Where(s => s.ProfessorId == _loggedInProfessorId.Id);
                }
                var selectedLesson = lessons.FirstOrDefault();
                var addEditLessonsWindow = new AddEditLessonsWindow(selectedLesson);
                var succesful = addEditLessonsWindow.ShowDialog();
                if ((bool)succesful)
                {
                    RefreshDataGrid();
                }
            }

            //var selectedIndex = dgLessons.SelectedIndex;

            //if (selectedIndex >= 0)
            //{
            //    var lessons = lessonService.GetAvailableLessons();

            //    addEditLessonWindow = new AddEditLessonsWindow(lessons[selectedIndex]);

            //    var successeful = addEditLessonWindow.ShowDialog();

            //    if ((bool)successeful)
            //    {
            //        RefreshDataGrid();
            //    }
            //}
        }


        //private void miDeleteLesson_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedLesson = dgLessons.SelectedItem as Lesson;

        //    if (selectedLesson != null)
        //    {
        //        lessonService.Delete(selectedLesson.Id);
        //        RefreshDataGrid();
        //    }
        //}


        private void miDeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            var selectedLesson = dgLessons.SelectedItem as Lesson;

            if (selectedLesson != null && _loggedInUser.UserType == EUserType.PROFESSOR && selectedLesson.Status == true)
            {
                MessageBox.Show("You cannot delete a reserved lesson.");
            }
            else if (selectedLesson != null && _loggedInUser.UserType == EUserType.PROFESSOR && selectedLesson.ProfessorId == _loggedInProfessorId.Id)
            {
                lessonService.Delete(selectedLesson.Id);
                RefreshDataGrid();
            }
            else if (selectedLesson != null && _loggedInUser.UserType == EUserType.ADMINISTRATOR)
            {
                lessonService.Delete(selectedLesson.Id);
                RefreshDataGrid();
            }
        }

      
        public void RefreshDataGrid()
        {
            if (_loggedInUser.UserType == EUserType.PROFESSOR)
            {
                List<Lesson> lessons = lessonService.GetAvailableLessons()
                                                    .Where(s => s.ProfessorId == _loggedInProfessorId.Id)
                                                    .Select(s => s)
                                                    .ToList();
                dgLessons.ItemsSource = lessons;
            }
            else if (_loggedInUser.UserType == EUserType.STUDENT)
            {
                if (_currentButton == "ReservedLessons")
                {
                    List<Lesson> lessons = lessonService.GetAvailableLessons()
                                                        .Where(s => s.StudentId == _loggedInStudentId.Id)
                                                        .Select(s => s)
                                                        .ToList();
                    dgLessons.ItemsSource = lessons;
                }
                else
                {
                    List<Lesson> lessons = lessonService.GetAvailableLessons().Where(s => s.Status == false).Select(s => s).ToList();
                    dgLessons.ItemsSource = lessons;
                }
            }
            else
            {
                List<Lesson> lessons = lessonService.GetAvailableLessons().Select(s => s).ToList();
                dgLessons.ItemsSource = lessons;
            }
        }



        private void dgLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
                LessonService lesService = new LessonService();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    // show all available lessons
                    dgLessons.ItemsSource = lesService.GetAvailableLessons();
                }
                else
                {
                    DateTime searchDate;
                    try
                    {
                        searchDate = DateTime.Parse(searchTerm);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Invalid date format. Please enter date in format 'dd/mm/yyyy'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    List<Lesson> filteredLessons = lesService.GetAvailableLessons()
                        .Where(les => les.Date.Date == searchDate.Date)
                        .ToList();

                    dgLessons.ItemsSource = filteredLessons;
                }
            }
        }
        private void txtSearch2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchTerm = txtSearch2.Text;
                LessonService lesService = new LessonService();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    
                    dgLessons.ItemsSource = lesService.GetAvailableLessons();
                }
                else
                {
                    List<Lesson> filteredProfesors = lesService.GetAvailableLessons()
                        .Where(les =>
                                les.Professor.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))


                        .ToList();

                    dgLessons.ItemsSource = filteredProfesors;
                }
            }
        }

    }

}


