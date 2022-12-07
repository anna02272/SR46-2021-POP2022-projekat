using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface ILanguageService
    {
        List<Language> GetAll();
        Language GetById(string id);
        List<Language> GetActiveLanguages();
        List<Language> GetActiveLanguagesByNameOfLanguage(string nameOfLanguage);
        List<Language> GetActiveLanguagesOrderedByNameOfLanguage();
        void Add(Language language);
        void Set(List<Language> languages);
        void Update(string id, Language language);
        void Delete(string id);
      
    }
}
