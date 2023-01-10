using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class School : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Address address;

        public Address Address
        {
            get => address;
            set
            {
                address = value;
                AddressId = address?.Id;
            }
        }

        public int? AddressId { get; set; }

        private Language language;

        public Language Language
        {
            get => language;
            set
            {
                language = value;
                LanguageId = language?.Id;
            }
        }

        public int? LanguageId { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }

        public School()
        {
            IsDeleted = false;
        }

        public object Clone()
        {
            return new School
            {
                Id = Id,
                Name = Name,
                Address = Address?.Clone() as Address,
                Language = Language?.Clone() as Language,
                IsDeleted = IsDeleted
            };
        }

        public string Error
        {
            get
            {
                //if (string.IsNullOrEmpty(Id))
                //{
                //    return "Id cannot be empty!";
                //}
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
                //if (columnName == "Id" && string.IsNullOrEmpty(Id))
                //{
                //    IsValid = false;
                //    return "Id cannot be empty!";
                //}
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

      
        //public Address address
        //{
        //    get
        //    {
        //        return address;
        //    }
        //    set
        //    {
        //        address = value;
        //        OnPropertyChanged("Address");
        //    }

        //}
     
        //public Language language
        //{
        //    get
        //    {
        //        return language;
        //    }
        //    set
        //    {
        //        language = value;
        //        OnPropertyChanged("Language");
        //    }

        //}
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(String propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    
}
}
