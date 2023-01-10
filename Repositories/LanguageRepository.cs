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
using System.Net;
using System.Data;

namespace SR46_2021_POP2022.Repositories
{
    class LanguageRepository : ILanguageRepository
    {

        public int Add(Language language)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Languages (NameOfLanguage, IsDeleted)
                    output inserted.Id
                    values (@NameOfLanguage, @IsDeleted)"
                ;

                command.Parameters.Add(new SqlParameter("NameOfLanguage", language.NameOfLanguage));
                command.Parameters.Add(new SqlParameter("IsDeleted", language.IsDeleted));

                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<Language> languages)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Languages set IsDeleted=1 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Language> GetAll()
        {
            List<Language> languages = new List<Language>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Languages";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Languages");

                foreach (DataRow row in ds.Tables["Languages"].Rows)
                {
                    var language = new Language
                    {
                        Id = (int)row["Id"],
                        NameOfLanguage = row["NameOfLanguage"] as string,
                        IsDeleted = (bool)row["IsDeleted"]

                    };

                    languages.Add(language);
                }
            }

            return languages;
        }

        public Language GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Languages where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Languages");
                if (ds.Tables["Languages"].Rows.Count > 0)
                {
                    var row = ds.Tables["Languages"].Rows[0];

                    var language = new Language
                    {
                        Id = (int)row["Id"],
                        NameOfLanguage = row["NameOfLanguage"] as string,
                        IsDeleted = (bool)row["IsDeleted"]
                    };

                    return language;
                }
            }

            return null;
        }

        public void Set(List<Language> languages)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Language language)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Languages set 
                       NameOfLanguage = @NameOfLanguage
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("NameOfLanguage", language.NameOfLanguage));
             
                command.ExecuteNonQuery();
            }
        }
    }
    }
    //private static List<Language> languages = new List<Language>();

    //public void Add(Language language)
    //{
    //    Data.Instance.Languages.Add(language);
    //    Data.Instance.Save();
    //}

    //public void Add(List<Language> newLanguages)
    //{
    //    Data.Instance.Languages.AddRange(newLanguages);
    //    Data.Instance.Save();
    //}

    //public void Set(List<Language> newLanguages)
    //{
    //    Data.Instance.Languages = newLanguages;
    //}

    //public void Delete(string id)
    //{
    //    Language language = GetById(id);

    //    if (language != null)
    //    {
    //        language.IsDeleted = true;
    //    }

    //    Data.Instance.Save();
    //}

    //public List<Language> GetAll()
    //{
    //    return Data.Instance.Languages;
    //}

    //public Language GetById(string id)
    //{
    //    return Data.Instance.Languages.Find(u => u.Id == id);
    //}

    //public void Update(string id, Language updatedLanguage)
    //{
    //    Data.Instance.Save();
    //}

    //public void Save()
    //{
    //IFormatter formatter = new BinaryFormatter();
    //using (Stream stream = new FileStream(Config.languagesFilePath, FileMode.Create, FileAccess.Write))
    //{
    //    formatter.Serialize(stream, languages);
    //}
    //}

