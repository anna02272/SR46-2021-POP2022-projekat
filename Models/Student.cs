using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Student : ICloneable
    {
        private User user;

        public User User
        {
            get => user;
            set
            {
                user = value;
                UserId = user.Email;
            }
        }
        public string UserId { get; set; }

        public object Clone()
        {
            return new Student
            {
                User = User.Clone() as User
            };
        }

        public override string ToString()
        {
            return $"[Student] {User.FirstName} {User.LastName}, {User.Email}";
        }


        //public Address _address;
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
