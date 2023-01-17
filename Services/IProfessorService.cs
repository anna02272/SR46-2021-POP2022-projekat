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
        Professor GetById(int id);
        List<Professor> GetActiveProfessors();
        List<Professor> GetActiveProfessorsByEmail(string email);
        List<Professor> GetActiveProfessorsOrderedByEmail();
        void Add(Professor professor);
        void Set(List<Professor> professors);
        void Update(int id, Professor professor);
        void Delete(int id);
        List<User> ListAllStudents();
     
       
    }
}
