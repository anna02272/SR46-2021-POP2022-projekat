using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
using SR46_2021_POP2022.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ShowAddressesWindow.xaml
    /// </summary>
    public partial class ShowAddressesWindow : Window
    {
        private AddressService addressService = new AddressService();
       

        public enum State { ADMINISTRATION, DOWNLOADING };
        State state;
        public Address SelectedAddress = null;

        public ShowAddressesWindow(State state = State.ADMINISTRATION)
        {
            InitializeComponent();
            this.state = state;

            if (state == State.DOWNLOADING)
            {
                miAddAddress.Visibility = Visibility.Collapsed;
                miUpdateAddress.Visibility = Visibility.Collapsed;
                miDeleteAddress.Visibility = Visibility.Collapsed;
            }
            else
            {
                miPickAddress.Visibility = Visibility.Hidden;
            }

            //dgAddresses.ItemsSource = Data.Instance.Addresses;
            dgAddresses.ItemsSource = addressService.GetActiveAddresses();

            dgAddresses.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        public ShowAddressesWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void miPickAddress_Click(object sender, RoutedEventArgs e)
        {
            SelectedAddress = dgAddresses.SelectedItem as Address;
            this.DialogResult = true;
            this.Close();
        }

        private void miAddAddress_Click(object sender, RoutedEventArgs e)
        {
            var addEditAddressWindow = new AddEditAddressesWindow();

            var successeful = addEditAddressWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateAddress_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgAddresses.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var addresses = addressService.GetActiveAddresses();

                var addEditAddressWindow = new AddEditAddressesWindow(addresses[selectedIndex]);

                var successeful = addEditAddressWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void miDeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            var selectedAddress = dgAddresses.SelectedItem as Address ;

            if (selectedAddress != null)
            {
                addressService.Delete(selectedAddress.Id);
                RefreshDataGrid();
            }
        }

       
        private void RefreshDataGrid()
        {
            List<Address> addresses = addressService.GetActiveAddresses().Select(p => p).ToList();
            dgAddresses.ItemsSource = addresses;
        }

        private void dgAddresses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
