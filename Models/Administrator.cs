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
        public string UserName { get; set; }
        public string Password { get; set; }
      

        public object Clone()
        {
            return new Administrator
            {
                Id = Id,
                UserName = UserName,
                Password = Password
               
            };
        }

        public override string ToString()
        {
            return $"{UserName} ";
        }
    }
}
