using SR46_2021_POP2022.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Lesson :ICloneable, IDataErrorInfo, INotifyPropertyChanged

    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Professor professor;
        public Professor Professor
        {
            get => professor;
            set
            {
                professor = value;
               ProfessorId = professor?.Id;
                OnPropertyChanged("Professor");
            }
        }
        public int? ProfessorId { get; set; }

        public DateTime Date { get; set; }

       

        //public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Status {get; set;}

          
        private Student student;
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                StudentId = student?.Id;
                OnPropertyChanged("Student");
            }
        }
        public int? StudentId { get; set; }

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
              
                if (Date == null && Date < DateTime.Now)
                {
                    return "Date must be in the future.";
                }
            
                if (Duration == null)
                {
                    return "Duration cannot be empty!";
                }
                //else if (!StudentId.HasValue)
                //{
                //    return "Student cannot be empty!";
                //}
                //else if (!ProfessorId.HasValue)
                //{
                //    return "Professor cannot be empty!";
                //}
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
                //}
                if (columnName == "Date" && Date == null)
                {
                    IsValid = false;
                    return "Date cannot be empty!";
                }
                else if (columnName == "Date" && Date < DateTime.Now)
                {
                    IsValid = false;
                    return "Date must be in the future.";
                }
                else if (columnName == "Duration" && Duration == null)
                {
                    IsValid = false;
                    return "Duration cannot be empty!";
                }
                //else if (columnName == "ProfessorId" && !ProfessorId.HasValue)
                   
                //{
                //    IsValid = false;
                //    return "Professor cannot be empty!";
                //}
                //else if (columnName == "StudentId" && !StudentId.HasValue)
                //{
                //    IsValid = false;
                //    return "Student cannot be empty!";
                //}
                return "";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }




        public override string ToString()
        {
           return $"{Name}, {Professor}  {Date}, {Duration}, {Status}, {Student}";
        }

      
    
       
       

    
    }
}

