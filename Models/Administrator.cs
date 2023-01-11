using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Administrator : ICloneable
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
                UserId = user.Id;
            }
        }

        public object Clone()
        {
            return new Administrator
            {
                Id = Id,
                User = User.Clone() as User
            };
        }

        public override string ToString()
        {
            return $"[Administrator] {User.FirstName} {User.LastName}, {User.Email}";
        }

    }
}