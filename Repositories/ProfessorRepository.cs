using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SR46_2021_POP2022.Repositories
{
    class ProfessorRepository : IProfessorRepository
    {
        public int Add(Professor professor)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Professors (UserId)
                    output inserted.Id
                    values (@UserId)";

                command.Parameters.Add(new SqlParameter("UserId", professor.UserId));

                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<Professor> professors)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Users set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Professor> GetAll()
        {
            List<Professor> professors = new List<Professor>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Professors p, dbo.Users u where p.UserId=u.id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Professors");

                foreach (DataRow row in ds.Tables["Professors"].Rows)
                {
                    var user = new User
                    {
                        Id = (int)row["UserId"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"],
                        AddressId = (int)row["AddressId"]


                    };

                    var professor = new Professor
                    {
                        Id = (int)row["id"],
                        User = user
                    };

                    professors.Add(professor);
                }
            }

            return professors;
        }

        public Professor GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Professors p, dbo.Users u where p.UserId=u.id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Professors");

                if (ds.Tables["Professors"].Rows.Count > 0)
                {
                    var row = ds.Tables["Professors"].Rows[0];

                    var user = new User
                    {
                        Id = (int)row["Id"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"],
                        AddressId = (int)row["AddressId"]
                    };

                    var professor = new Professor
                    {
                        User = user
                    };

                    return professor;
                }
            }

            return null;
        }

        public void Set(List<Professor> professors)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Professor professor)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Professors 
                        set UserId = @UserId
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("UserId", professor.UserId));

                command.ExecuteScalar();
            }
        }
    }
    }


//        public void Add(Professor professor)
//        {
//            Data.Instance.Professors.Add(professor);
//            Data.Instance.Save();
//        }

//        public void Add(List<Professor> newProfessors)
//        {
//            Data.Instance.Professors.AddRange(newProfessors);
//            Data.Instance.Save();
//        }

//        public void Set(List<Professor> newProfessors)
//        {
//            Data.Instance.Professors = newProfessors;
//        }

//        public void Delete(string email)
//        {
//            Professor professor = GetById(email);

//            if (professor != null)
//            {
//                professor.User.IsActive = false;
//            }

//            Data.Instance.Save();
//        }

//        public List<Professor> GetAll()
//        {
//            return Data.Instance.Professors;
//        }

//        public Professor GetById(string email)
//        {
//            return Data.Instance.Professors.Find(u => u.User.Email == email);
//        }

//        public void Update(string email, Professor updatedProfessor)
//        {
//            Data.Instance.Save();
//        }
//    }
//}

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