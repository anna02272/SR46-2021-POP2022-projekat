using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Address : ICloneable
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsNotDeleted { get; set; }
        public bool IsValid { get; set; }

        public Address()
        {
            IsNotDeleted = true;
        }

        public object Clone()
        {
            return new Address
            {
                Id = Id,
                Street = Street,
                StreetNumber = StreetNumber,
                City = City,
                Country = Country,
                IsNotDeleted   = IsNotDeleted
            };
        }
        public string Error
        {
            get
            {
               
                if (string.IsNullOrEmpty(Street))
                {
                    return "Street cannot be empty!";
                }
                else if (string.IsNullOrEmpty(StreetNumber))
                {
                    return "Street number cannot be empty!";
                }
                else if (string.IsNullOrEmpty(City))
                {
                    return "City cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Country))
                {
                    return "Country cannot be empty!";
                }

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Street" && string.IsNullOrEmpty(Street))
                {
                    IsValid = false;
                    return "Street cannot be empty!";
                }
                else if (columnName == "StreetNumbet" && string.IsNullOrEmpty(StreetNumber))
                {
                    IsValid = false;
                    return "Street number cannot be empty!";
                }
                else if (columnName == "City" && string.IsNullOrEmpty(City))
                {
                    IsValid = false;
                    return "City cannot be empty!";
                }
                else if (columnName == "Country" && string.IsNullOrEmpty(Country))
                {
                    IsValid = false;
                    return "Country cannot be empty!";
                }
       

                return "";
            }
        }
        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City}, {Country}";
        }
    }
}
