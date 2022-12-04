using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Professor : ICloneable
    {
        private User user;

        public User User
        {
            get => user;
            set
            {
                user = value;
                UserId = user.Email; // kada se setuje User tada se setuje i UserId, tako ne moramo kasnije da ih setujemo zasebno
            }
        }
        public string UserId { get; set; }

        public object Clone()
        {
            return new Professor
            {
                User = User.Clone() as User
            };
        }

        public override string ToString()
        {
            return $"[Professor] {User.FirstName} {User.LastName}, {User.Email}";
        }
    }
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
