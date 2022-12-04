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
    class ProfessorRepository : IProfessorRepository
    {
        public void Add(Professor professor)
        {
            Data.Instance.Professors.Add(professor);
            Data.Instance.Save();
        }

        public void Add(List<Professor> newProfessors)
        {
            Data.Instance.Professors.AddRange(newProfessors);
            Data.Instance.Save();
        }

        public void Set(List<Professor> newProfessors)
        {
            Data.Instance.Professors = newProfessors;
        }

        public void Delete(string email)
        {
            Professor professor = GetById(email);

            if (professor != null)
            {
                professor.User.IsActive = false;
            }

            Data.Instance.Save();
        }

        public List<Professor> GetAll()
        {
            return Data.Instance.Professors;
        }

        public Professor GetById(string email)
        {
            return Data.Instance.Professors.Find(u => u.User.Email == email);
        }

        public void Update(string email, Professor updatedProfessor)
        {
            Data.Instance.Save();
        }
    }
}

//    class ProfessorRepository : IProfessorRepository, IFilePersistence
//    {
//        private static List<Professor> professors = new List<Professor>();

//        public void Add(Professor professor)
//        {
//            professors.Add(professor);
//            Save();
//        }

//        public void Add(List<Professor> newProfessors)
//        {
//            professors.AddRange(newProfessors);
//            Save();
//        }

//        public void Set(List<Professor> newProfessors)
//        {
//            professors = newProfessors;
//        }

//        public void Delete(string email)
//        {
//            Professor professor = GetById(email);

//            if (professor != null)
//            {
//                professor.User.IsActive = false;
//            }

//            Save();
//        }

//        public List<Professor> GetAll()
//        {
//            return professors;
//        }

//        public Professor GetById(string email)
//        {
//            return professors.Find(u => u.User.Email == email);
//        }

//        public void Update(string email, Professor updatedProfessor)
//        {
//            Save();
//        }

//        public void Save()
//        {
//            IFormatter formatter = new BinaryFormatter();
//            using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Create, FileAccess.Write))
//            {
//                formatter.Serialize(stream, professors);
//            }
//        }
//        public List<Professor> Search(string search)
//        {
//            search = search.ToLower();

//            return professors
//                .Where(p => p.User.FirstName.ToLower().Contains(search) ||
//            p.User.LastName.ToLower().Contains(search) || 
//            p.User.Email.ToLower().Contains(search) ||
//            (p.User.JMBG != null && p.User.JMBG.ToLower().Contains(search)))
//                .Where(p => p.User.IsActive).ToList();
//        }
//    }
//}