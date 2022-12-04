using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    class StudentRepository : IStudentRepository
    {

        public void Add(Student student)
        {
            Data.Instance.Students.Add(student);
            Data.Instance.Save();
        }

        public void Add(List<Student> newStudents)
        {
            Data.Instance.Students.AddRange(newStudents);
            Data.Instance.Save();

        }
        public void Set(List<Student> newStudents)
        {
            Data.Instance.Students = newStudents;
        }
        public void Delete(string email)
        {
            Student student = GetById(email);

            if (student != null)
            {
                student.User.IsActive = false;
            }
            Data.Instance.Save();
        }

        public List<Student> GetAll()
        {
            return Data.Instance.Students;
        }

        public Student GetById(string email)
        {
            return Data.Instance.Students.Find(u => u.User.Email == email);

        }

        public void Update(string email, Student updatedStudent)
        {
            Data.Instance.Save();
        }

        //public void Save()
        //{
            //IFormatter formatter = new BinaryFormatter();
            //using (Stream stream = new FileStream(Config.studentsFilePath, FileMode.Create, FileAccess.Write))
            //{
            //    formatter.Serialize(stream, students);
            //}
        //}

    }
}
