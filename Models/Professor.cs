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
        private School school;
        public School School
        {
            get => school;
            set
            {
                school = value;
                SchoolId = school?.Id;

            }
        }
        public int? SchoolId { get; set; }
       

        public object Clone()
        {
            return new Professor
            {
                Id = Id,
                User = User.Clone() as User,
                School = School?.Clone() as School
               
            };
        }

        public override string ToString()
        {
            return $"{User.FirstName} {User.LastName}";
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
}
