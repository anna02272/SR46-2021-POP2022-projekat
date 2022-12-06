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
    class LanguageRepository : ILanguageRepository
    {
        //private static List<Language> languages = new List<Language>();

        public void Add(Language language)
        {
            Data.Instance.Languages.Add(language);
            Data.Instance.Save();
        }

        public void Add(List<Language> newLanguages)
        {
            Data.Instance.Languages.AddRange(newLanguages);
            Data.Instance.Save();
        }

        public void Set(List<Language> newLanguages)
        {
            Data.Instance.Languages = newLanguages;
        }

        public void Delete(int id)
        {
            Language language = GetById(id);

            if (language != null)
            {
                language.IsDeleted = true;
            }

            Data.Instance.Save();
        }

        public List<Language> GetAll()
        {
            return Data.Instance.Languages;
        }

        public Language GetById(int id)
        {
            return Data.Instance.Languages.Find(u => u.Id == id);
        }

        public void Update(int id, Language updatedLanguage)
        {
            Data.Instance.Save();
        }

        //public void Save()
        //{
            //IFormatter formatter = new BinaryFormatter();
            //using (Stream stream = new FileStream(Config.languagesFilePath, FileMode.Create, FileAccess.Write))
            //{
            //    formatter.Serialize(stream, languages);
            //}
        //}
    }
}