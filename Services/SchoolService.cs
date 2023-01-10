using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    class SchoolService : ISchoolService
    {
        private ISchoolRepository schoolRepository;

        public SchoolService()
        {
            schoolRepository = new SchoolRepository();
          
        }

        public School GetById(int id)
        {
            return schoolRepository.GetById(id);
        }

        public List<School> GetAll()
        {
            return schoolRepository.GetAll();
        }

        public void Add(School school)
        {
            schoolRepository.Add(school);
           
        }

        public void Set(List<School> schools)
        {
            schoolRepository.Set(schools);
        }

        public void Update(int id, School school)
        {
            schoolRepository.Update(id, school);
        }

        public void Delete(int id)
        {
        
            schoolRepository.Delete(id);
        }

        public List<School> GetActiveSchools()
        {
            return schoolRepository.GetAll().Where(p => !p.IsDeleted).ToList();
        }

        public List<School> GetActiveSchoolsByName(string name)
        {
            return schoolRepository.GetAll().Where(p => !p.IsDeleted && p.Name.Contains(name)).ToList();
        }

        public List<School> GetActiveSchoolsOrderedByName()
        {
            return schoolRepository.GetAll().Where(p => !p.IsDeleted).OrderBy(p => p.Name).ToList();
        }

    }
}

