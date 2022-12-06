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
    class SchoolRepository : ISchoolRepository
    {
        //private static List<School> schools = new List<School>();

        public void Add(School school)
        {
            Data.Instance.Schools.Add(school);
            Data.Instance.Save();
        }

        public void Add(List<School> newSchools)
        {
            Data.Instance.Schools.AddRange(newSchools);
            Data.Instance.Save();
        }

        public void Set(List<School> newSchools)
        {
            Data.Instance.Schools = newSchools;
        }

        public void Delete(int id)
        {
           School school = GetById(id);

            if (school != null)
            {
                school.IsDeleted = true;
            }

            Data.Instance.Save();


        }

        public List<School> GetAll()
        {
            return Data.Instance.Schools;
        }

        public School GetById(int id)
        {
            return Data.Instance.Schools.Find(u => u.Id == id);
        }

        public void Update(int id,School updatedSchool)
        {
            Data.Instance.Save();
        }

        //public void Save()
        //{
            //IFormatter formatter = new BinaryFormatter();
            //using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Create, FileAccess.Write))
            //{
            //    formatter.Serialize(stream, schools);
            //}
        //}
    }
}