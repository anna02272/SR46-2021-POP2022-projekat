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
    class SchoolRepository : ISchoolRepository
    {
        public int Add(School school)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Schools (Name, AddressId, LanguageId, IsDeleted)
                    output inserted.Id
                    values (@Name, @AddressId, @LanguageId, @IsDeleted)";

                command.Parameters.Add(new SqlParameter("Name", school.Name));
                command.Parameters.Add(new SqlParameter("AddressId", (object)school.AddressId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("LanguageId", (object)school.LanguageId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("IsDeleted", school.IsDeleted));


                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<School> schools)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Schools set IsDeleted=1 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

       
        public List<School> GetAll()
        {
            List<School> schools = new List<School>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select s.*, a.*, l.* from dbo.Schools s left join dbo.Addresses a" +
                    " on s.AddressId = a.Id left join dbo.Languages l on s.LanguageId = l.Id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Schools");

                foreach (DataRow row in ds.Tables["Schools"].Rows)
                {
                    var school = new School
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        AddressId = (int)row["AddressId"],
                        LanguageId = (int)row["LanguageId"],
                     
                    };
                    school.Address = new Address
                    {
                        Id = (int)row["AddressId"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string
                    };
                    school.Language = new Language
                    {
                        Id = (int)row["LanguageId"],
                        NameOfLanguage = row["NameOfLanguage"] as string,
                        
                    };

                    schools.Add(school);
                }
            }
            return schools;
        }


        public School GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Schools where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Schools");
                if (ds.Tables["Schools"].Rows.Count > 0)
                {
                    var row = ds.Tables["Schools"].Rows[0];

                    var school = new School
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        AddressId = (int)row["AddressId"],
                        LanguageId = (int)row["LanguageId"],
                        IsDeleted = (bool)row["IsDeleted"]
                    };

                    return school;
                }
            }

            return null;
        }

        public void Set(List<School> schools)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, School school)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Schools set 
                        Name = @Name,
                        AddressId = @AddressId,
                        LanguageId = @LanguageId
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("Name", school.Name));
                command.Parameters.Add(new SqlParameter("AddressId", school.AddressId));
                command.Parameters.Add(new SqlParameter("LanguageId", school.LanguageId));

                command.ExecuteScalar();
            }
        }
    }
    
}
//private static List<School> schools = new List<School>();

//public void Add(School school)
//{
//    Data.Instance.Schools.Add(school);
//    Data.Instance.Save();
//}

//public void Add(List<School> newSchools)
//{
//    Data.Instance.Schools.AddRange(newSchools);
//    Data.Instance.Save();
//}

//public void Set(List<School> newSchools)
//{
//    Data.Instance.Schools = newSchools;
//}

//public void Delete(string id)
//{
//   School school = GetById(id);

//    if (school != null)
//    {
//        school.IsDeleted = true;
//    }

//    Data.Instance.Save();


//}

//public List<School> GetAll()
//{
//    return Data.Instance.Schools;
//}

//public School GetById(string id)
//{
//    return Data.Instance.Schools.Find(u => u.Id == id);
//}

//public void Update(string id,School updatedSchool)
//{
//    Data.Instance.Save();
//}

//public void Save()
//{
//IFormatter formatter = new BinaryFormatter();
//using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Create, FileAccess.Write))
//{
//    formatter.Serialize(stream, schools);
//}
//}
