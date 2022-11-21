using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; } 
        public List<Language> Languages { get; set; } 
      

        public override string ToString()
        {
            return $"{Name} {Address}, {Languages}";
        }
    }
}
