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
            var selectedIndex = dgLessons.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var lessons = lessonService.GetAll();

                addEditLessonWindow = new AddEditLessonsWindow(lessons[selectedIndex]);

                var successeful = addEditLessonWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            var selectedLesson = dgLessons.SelectedItem as Lesson;

            if (selectedLesson != null)
            {
                lessonService.Delete(selectedLesson.Id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<Lesson> lessons = lessonService.GetAvailableLessons().Select(p => p).ToList();
            dgLessons.ItemsSource = lessons;
        }

        private void dgLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
