using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Professor : ICloneable
    {

        public int Id { get; set; }
        private User user;
        public int UserId { get; set; }

        public User User
        {
            get => user;
            set
            {
                user = value;
                UserId = user.Id; // kada se setuje User tada se setuje i UserId, tako ne moramo kasnije da ih setujemo zasebno
            }
        }
       

        public object Clone()
        {
            return new Professor
            {
                Id = Id,
                User = User.Clone() as User
            };
        }

        public override string ToString()
        {
            return $"[Professor] {User.FirstName} {User.LastName}, {User.Email}";
        }

        //public Address _address;
        //public Address address
        //{
        //    get
        //    {
        //        return _address;
        //    }
        //    set
        //    {
        //        _address = value;
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
  

    //[Serializable]
    //class Professor
    //{
    //    [NonSerialized]
    //    private User user;

    //    public User User { get => user; set => user = value; }
    //    public string UserId { get; set; }

    //    public override string ToString()
    //    {
    //        return $"[Professor] {User.FirstName} {User.LastName}, {User.Email}";
    //    }
    //}
}
}
