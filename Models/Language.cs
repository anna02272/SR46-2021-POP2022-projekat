using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models

{
    [Serializable]

    class Language

    {
        public int Id { get; set; } 
        public string NameOfLanguage { get; set; }


        public override string ToString()
        {
            return $"{NameOfLanguage}";
        }
    }
}
