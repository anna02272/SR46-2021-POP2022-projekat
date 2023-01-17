using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
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
using System.Xml.Linq;

namespace SR46_2021_POP2022.Views
{
    /// <summary>
    /// Interaction logic for AddEditLanguagesWindow.xaml
    /// </summary>
    public partial class AddEditLanguagesWindow : Window
    {
        private Language language;

        private ILanguageService languageService = new LanguageService();

        private bool isAddMode;

        public AddEditLanguagesWindow(Language language)
        {
            InitializeComponent();
            this.language = language.Clone() as Language;

            DataContext = this.language;

          
            isAddMode = false;
            //txtId.IsReadOnly = true;

        }

        public AddEditLanguagesWindow()
        {
            InitializeComponent();

            language = new Language
            {

                IsDeleted = false,
                IsValid = true,

            };



            isAddMode = true;
            DataContext = language;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {


            if (language.IsValid)
            {
                if (isAddMode)
                {
                    languageService.Add(language);
                }
                else
                {
                    languageService.Update(language.Id, language);
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
    }
}
