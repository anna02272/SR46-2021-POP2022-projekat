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
        Student GetById(string email);
        List<Student> GetActiveStudents();
        List<Student> GetActiveStudentsByEmail(string email);
        List<Student> GetActiveStudentsOrderedByEmail();
        void Add(User student);
        void Set(List<Student> students);
        void Update(string email, Student student);
        void Delete(string email);
        List<User> ListAllProfessors();
    }
}
//ja sam ovdee
