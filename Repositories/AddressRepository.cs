using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace SR46_2021_POP2022.Repositories
    {
        class AddressRepository : IAddressRepository
        {
            //private static List<Address> addresses = new List<Address>();

            public void Add(Address address)
            {
                Data.Instance.Addresses.Add(address);
                Data.Instance.Save();
             }

            public void Add(List<Address> newAddresses)
            {
                Data.Instance.Addresses.AddRange(newAddresses);
                Data.Instance.Save();
             }

            public void Set(List<Address> newAddresses)
            {
                Data.Instance.Addresses = newAddresses;
             }

            public void Delete(int id)
            {
                Address address = GetById(id);

                if (address != null)
                {
                    address.IsDeleted = true;
            }

                Data.Instance.Save();
           
            }

            public List<Address> GetAll()
            {
                 return Data.Instance.Addresses;
              }

            public Address GetById(int id)
            {
                return Data.Instance.Addresses.Find(u => u.Id == id);
            }

            public void Update(int id, Address updatedAddress)
            {
                Data.Instance.Save();
            }

            //public void Save()
            //{
                //IFormatter formatter = new BinaryFormatter();
                //using (Stream stream = new FileStream(Config.addressesFilePath, FileMode.Create, FileAccess.Write))
                //{
                //    formatter.Serialize(stream, addresses);
                //}
            //}
        }
    }


