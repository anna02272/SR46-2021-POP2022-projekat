﻿using SR46_2021_POP2022.Models;
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
    /// Interaction logic for AddEditSchoolsWindow.xaml
    /// </summary>
    public partial class AddEditSchoolsWindow : Window
    {
        private School school;
        private ISchoolService schoolService = new SchoolService();
        private bool isAddMode;

        public AddEditSchoolsWindow(School school)
        {
            InitializeComponent();
            this.school = school.Clone() as School;

            DataContext = this.school;

            isAddMode = false;
            txtId.IsReadOnly = true;
           
        }

        public AddEditSchoolsWindow()
        {
            InitializeComponent();

            var school = new School
            {
               
                IsNotDeleted = true
            };

           
            isAddMode = true;
            DataContext = school;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (school.IsValid)
            {
                if (isAddMode)
                {
                    schoolService.Add(school);
                }
                else
                {
                    schoolService.Update(school.Id, school);
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
