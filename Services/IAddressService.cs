using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface IAddressService

    {
        List<Address> GetAll();
        Address GetById(int id);
        List<Address> GetActiveAddresses();
        List<Address> GetActiveAddressesByCountry(string country);
        List<Address> GetActiveAddressesOrderedByCountry();
        void Add(Address address);
        void Set(List<Address> addresses);
        void Update(int id, Address address);
        void Delete(int id);
       
    }
}
