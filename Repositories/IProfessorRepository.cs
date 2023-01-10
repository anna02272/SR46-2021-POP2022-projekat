using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface IProfessorRepository
    {
        List<Professor> GetAll();
        Professor GetById(int id);
        int Add(Professor professor);
        void Add(List<Professor> professors);
        void Set(List<Professor> professors);
        void Update(int id, Professor professor);
        void Delete(int id);
        //List<Professor> Search(string search);
    }
}
