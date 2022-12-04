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
    /// Interaction logic for ShowLanguagesWindow.xaml
    /// </summary>
    public partial class ShowLanguagesWindow : Window
    {
        private LanguageService languageService = new LanguageService();

        public ShowLanguagesWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void miAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            var addEditLanguageWindow = new AddEditLanguagesWindow();

            var successeful = addEditLanguageWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateLanguage_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgLanguages.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var languages = languageService.GetAll();

                var addEditLanguageWindow = new AddEditLanguagesWindow(languages[selectedIndex]);

                var successeful = addEditLanguageWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteLanguage_Click(object sender, RoutedEventArgs e)
        {
            var selectedLanguage = dgLanguages.SelectedItem as Language;

            if (selectedLanguage != null)
            {
                languageService.Delete(selectedLanguage.Id);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<Language> languages = languageService.GetAll().ToList();
            dgLanguages.ItemsSource = languages;
        }

        private void dgLanguages_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}

