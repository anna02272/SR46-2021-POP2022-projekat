using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(int id);
        int Add(Student student);
        void Add(List<Student> students);
        void Set(List<Student> students);
        void Update(int id, Student student);
        void Delete(int id);
    }
}
