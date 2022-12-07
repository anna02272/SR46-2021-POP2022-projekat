using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Lesson :ICloneable, IDataErrorInfo

    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Status {get; set;}
        public Student Student { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }

        public Lesson()
        {
            IsDeleted = false;
            Status = false;
        }

        public object Clone()
        {
            return new Lesson
            {
                Id = Id,
                Name = Name,
                Professor = Professor?.Clone() as Professor,
                Date = Date,
                Time = Time, 
                Duration = Duration,
                Status = Status, 
                Student = Student?.Clone() as Student,
                IsDeleted = IsDeleted

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
                if (columnName == "Id" && string.IsNullOrEmpty(Id))
                {
                    IsValid = false;
                    return "Id cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Id))
                {
                    return "Id cannot be empty!";
                }
                else if (columnName == "Name" && string.IsNullOrEmpty(Name))
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

      
        //public Student student
        //{
        //    get
        //    {
        //        return student;
        //    }
        //    set
        //    {
        //        student = value;
        //        OnPropertyChanged("Student");
        //    }

        //}
       
        //public Professor professor
        //{
        //    get
        //    {
        //        return professor;
        //    }
        //    set
        //    {
        //        professor = value;
        //        OnPropertyChanged("Professor");
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

