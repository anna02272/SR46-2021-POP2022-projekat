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
    /// Interaction logic for AddEditAddressesWindow.xaml
    /// </summary>
    public partial class AddEditAddressesWindow : Window
    {
        private Address address;
        private IAddressService addressService = new AddressService();
        private bool isAddMode;


        public AddEditAddressesWindow(Address address)
        {
            InitializeComponent();
            this.address = address.Clone() as Address;

            DataContext = this.address;

            isAddMode = false;
            txtId.IsReadOnly = true;
            
        }

        public AddEditAddressesWindow()
        {
            InitializeComponent();

            address = new Address
            {
                IsDeleted = false
            };

           

            isAddMode = true;
            DataContext = address;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (address.IsValid)
            {
                if (isAddMode)
                {
                    addressService.Add(address);
                }
                else
                {
                    addressService.Update(address.Id, address);
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
