using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    class Lesson
    {
        public int Id { get; set; }
        public Professor Professor { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Status {get; set;}
        public Student Student { get; set; }

        public override string ToString()
        {
           return $"{Professor} {Date}, {Time}, {Duration}, {Status}, {Student}";
        }
    }
}

