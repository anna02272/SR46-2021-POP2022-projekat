using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface IAddressRepository
    {
      
            List<Address> GetAll();
            Address GetById(int id);
            void Add(Address address);
            void Add(List<Address> addresses);
            void Set(List<Address> addresses);
            void Update(int id, Address address);
            void Delete(int id);
        
    }
}
