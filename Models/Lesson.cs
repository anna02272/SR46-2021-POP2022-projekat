﻿using SR46_2021_POP2022.Properties;
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
    public class Lesson :ICloneable, IDataErrorInfo

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
                if (Professor == null)
                {
                    return "Professor cannot be empty!";
                }
                if (Student == null)
                {
                    return "Student cannot be empty!";
                }
                if (Date == null)
                {
                    return "Date cannot be empty!";
                }
                if (Duration == null)
                {
                    return "Duration cannot be empty!";
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
                if (columnName == "Professor" && Professor == null)
                {
                    IsValid = false;
                    return "Professor cannot be empty!";
                }
                if (columnName == "Student" && Student == null)
                {
                    IsValid = false;
                    return "Student cannot be empty!";
                }
                if (columnName == "Date" && Date == null)
                {
                    IsValid = false;
                    return "Date cannot be empty!";
                }
                if (columnName == "Duration" && Duration == null)
                {
                    IsValid = false;
                    return "Duration cannot be empty!";
                }
                return "";
            }
        }

    
       
   

    public override string ToString()
        {
           return $"{Name}, {Professor}  {Date}, {Duration}, {Status}, {Student}";
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

