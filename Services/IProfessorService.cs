using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface IProfessorService
    {
        List<Professor> GetAll();
        Professor GetById(string email);
        List<Professor> GetActiveProfessors();
        List<Professor> GetActiveProfessorsByEmail(string email);
        List<Professor> GetActiveProfessorsOrderedByEmail();
        void Add(User professor);
        void Set(List<Professor> professors);
        void Update(string email, Professor professor);
        void Delete(string email);
        List<User> ListAllStudents();
    }
}
