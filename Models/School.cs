using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class School : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; } 
        public Language Language { get; set; }
        public bool IsNotDeleted { get; set; }
        public bool IsValid { get; set; }

        public School()
        {
            IsNotDeleted = true;
        }

        public object Clone()
        {
            return new School
            {
                Id = Id,
                Name = Name,
                Address = Address,
                Language = Language,
                IsNotDeleted = IsNotDeleted
            };
        }

        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Name cannot be empty!";
                }
               

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Name" && string.IsNullOrEmpty(Name))
                {
                    IsValid = false;
                    return "Name cannot be empty!";
                }
              
               

                return "";
            }
        }

        public override string ToString()
        {
            return $"{Name} {Address}, {Language}";
        }
    }
}
