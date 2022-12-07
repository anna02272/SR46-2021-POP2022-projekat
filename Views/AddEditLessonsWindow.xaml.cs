using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Services;
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
using System.Windows.Shapes;

namespace SR46_2021_POP2022.Views
{
    /// <summary>
    /// Interaction logic for AddEditLessonsWindow.xaml
    /// </summary>
    public partial class AddEditLessonsWindow : Window
    {
        private Lesson lesson;
        private ILessonService lessonService = new LessonService();
        private bool isAddMode;

        public AddEditLessonsWindow(Lesson lesson)
        {
            InitializeComponent();
            this.lesson = lesson.Clone() as Lesson;

            DataContext = lesson;

            isAddMode = false;
            txtId.IsReadOnly = true;

        }

        public AddEditLessonsWindow()
        {
            InitializeComponent();

            lesson = new Lesson
            {

                IsDeleted = false
            };



            isAddMode = true;
            DataContext = lesson;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (lesson.IsValid)
            {
                if (isAddMode)
                {
                    lessonService.Add(lesson);
                }
                else
                {
                    lessonService.Update(lesson.Id, lesson);
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

        private void btnPickStudent_Click(object sender, RoutedEventArgs e)
        {
            ShowStudentsWindow aw = new ShowStudentsWindow(ShowStudentsWindow.State.DOWNLOADING);
            if (aw.ShowDialog() == true)
            {
                lesson.Student = aw.SelectedStudent;

            }
        }
        private void btnPickProfessor_Click(object sender, RoutedEventArgs e)
        {
            ShowProfessorsWindow aw = new ShowProfessorsWindow(ShowProfessorsWindow.State.DOWNLOADING);
            if (aw.ShowDialog() == true)
            {
                lesson.Professor = aw.SelectedProfessor;

            }
        }
    }
}

    

