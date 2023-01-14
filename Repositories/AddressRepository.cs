using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SR46_2021_POP2022.Repositories
    {
    class AddressRepository : IAddressRepository
    {

        public int Add(Address address)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Addresses(Street, StreetNumber, City, Country, IsDeleted)
                       output inserted.Id 
                       values (@Street, @StreetNumber, @City, @Country, @IsDeleted)";

                command.Parameters.Add(new SqlParameter("Street", address.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", address.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", address.City));
                command.Parameters.Add(new SqlParameter("Country", address.Country));
                command.Parameters.Add(new SqlParameter("IsDeleted", address.IsDeleted )); 

                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<Address> addresses)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Addresses set IsDeleted=1 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();  
            }

        }

        public List<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Addresses";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Addresses");

                foreach (DataRow row in ds.Tables["Addresses"].Rows)
                {
                    var address = new Address
                    {
                        Id = (int)row["Id"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                        IsDeleted = (bool)row["IsDeleted"]
                    };

                    addresses.Add(address);
                }
            }

            return addresses;
        }
    
        

        public Address GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Addresses where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Addresses");

                if (ds.Tables["Addresses"].Rows.Count > 0)
                {
                    var row = ds.Tables["Addresses"].Rows[0];
                    var address = new Address
                    {
                        Id = (int)row["Id"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                        IsDeleted = (bool)row["IsDeleted"]
                    };

                   return address;
                }
            }
            return null;
        }

        public void Set(List<Address> addresses)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Address address)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Addresses set 
                        Street = @Street,
                        StreetNumber = @StreetNumber,
                        City = @City,
                        Country = @Country
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("Street", address.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", address.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", address.City));
                command.Parameters.Add(new SqlParameter("Country", address.Country));
             

                command.ExecuteScalar();
            }
        }
    }

    //private static List<Address> addresses = new List<Address>();

    //public void Add(Address address)
    //{
    //    Data.Instance.Addresses.Add(address);
    //    Data.Instance.Save();
    // }

    //public void Add(List<Address> newAddresses)
    //{
    //    Data.Instance.Addresses.AddRange(newAddresses);
    //    Data.Instance.Save();
    //}


    //public void Set(List<Address> newAddresses)
    //{
    //    Data.Instance.Addresses = newAddresses;
    //}

    //public void Delete(string id)
    //    {
    //        Address address = GetById(id);

    //        if (address != null)
    //        {
    //            address.IsDeleted = true;
    //    }

    //        Data.Instance.Save();

    //    }

    //public List<Address> GetAll()
    //{
    //    return Data.Instance.Addresses;
    //}


    //public Address GetById(string id)
    //{
    //    return Data.Instance.Addresses.Find(u => u.Id == id);
    //}


    //public void Update(string id, Address updatedAddress)
    //    {
    //        Data.Instance.Save();
    //    }

    //public void Save()
    //{
    //IFormatter formatter = new BinaryFormatter();
    //using (Stream stream = new FileStream(Config.addressesFilePath, FileMode.Create, FileAccess.Write))
    //{
    //    formatter.Serialize(stream, addresses);
    //}
    //}

}


