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
    /// Interaction logic for ShowUsersWindow.xaml
    /// </summary>
    public partial class ShowUsersWindow : Window
    {
        private UserService userService = new UserService();


        public ShowUsersWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
        }


        private void RefreshDataGrid()
        {
            List<User> users = userService.GetActiveUsers().Select(p => p).ToList();
            dgUsers.ItemsSource = users;
        }

        private void dgUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
                UserService userService = new UserService();
                List<User> filteredUsers = userService.GetActiveUsers()
                    .Where(user => user.FirstName.ToLower().Contains(searchTerm.ToLower())
                                 || user.LastName.ToLower().Contains(searchTerm.ToLower())
                                 || user.Email.ToLower().Contains(searchTerm.ToLower())
                                 || user.UserType.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase)
                             || user.Address.ToString().Equals(searchTerm, StringComparison.OrdinalIgnoreCase))


                    .ToList();

                dgUsers.ItemsSource = filteredUsers;
            }
        }

    }
}
