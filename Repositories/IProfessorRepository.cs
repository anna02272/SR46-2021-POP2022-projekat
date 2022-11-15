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
        Professor GetById(string email);
        void Add(Professor professor);
        void Add(List<Professor> professors);
        void Set(List<Professor> professors);
        void Update(string email, Professor professor);
        void Delete(string email);
    }
}
