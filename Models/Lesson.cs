using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Lesson :ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Status {get; set;}
        public Student Student { get; set; }
        public bool IsNotDeleted { get; set; }
        public bool IsValid { get; set; }

        public Lesson()
        {
            IsNotDeleted = true;
            Status = false;
        }

        public object Clone()
        {
            return new Lesson
            {
                Id = Id,
                Name = Name,
                Professor = Professor,
                Date = Date,
                Time = Time, 
                Duration = Duration,
                Status = Status, 
                Student = Student,
                IsNotDeleted = IsNotDeleted

            };
        }
        public string Error
        {
            get
            {
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
           return $"{Name}, {Professor}  {Date}, {Time}, {Duration}, {Status}, {Student}";
        }
    }
}

