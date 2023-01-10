using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface IStudentService
    {
        List<Student> GetAll();
        Student GetById(int id);
        List<Student> GetActiveStudents();
        List<Student> GetActiveStudentsByEmail(string email);
        List<Student> GetActiveStudentsOrderedByEmail();
        void Add(Student student);
        void Set(List<Student> students);
        void Update(int id, Student student);
        void Delete(int id);
        List<User> ListAllProfessors();
    }
}

