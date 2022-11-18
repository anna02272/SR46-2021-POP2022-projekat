﻿using SR46_2021_POP2022.Models;
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
    /// Interaction logic for AddEditStudentsWindow.xaml
    /// </summary>
    public partial class AddEditStudentsWindow : Window
    {
        private User newUser;

        public AddEditStudentsWindow()
        {
            InitializeComponent();
            newUser = new User
            {
                UserType = EUserType.STUDENT,

            };

            DataContext = newUser;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.StudentService.Add(newUser);

            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}