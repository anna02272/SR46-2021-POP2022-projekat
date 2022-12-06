using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    class LanguageService : ILanguageService
    {
        private ILanguageRepository languageRepository;

        public LanguageService()
        {
            languageRepository = new LanguageRepository();

        }

        public Language GetById(int id)
        {
            return languageRepository.GetById(id);
        }

        public List<Language> GetAll()
        {
            return languageRepository.GetAll();
        }
        public List<Language> GetActiveLanguages()
        {
            return languageRepository.GetAll().Where(p => !p.IsDeleted).ToList();
        }


        public List<Language> GetActiveLanguagesOrderedByNameOfLanguage()
        {
            return languageRepository.GetAll().Where(p => !p.IsDeleted).OrderBy(p => p.NameOfLanguage).ToList();
        }
        public void Add(Language language)
        {
            languageRepository.Add(language);

        }
        public void Set(List<Language> languages)
        {
             languageRepository.Set(languages);
        }

        public void Update(int id, Language language)
        {
            languageRepository.Update(id, language);
        }

        public void Delete(int id)
        {

            languageRepository.Delete(id);
        }

        public List<Language> GetActiveLanguagesByNameOfLanguage(string nameOfLanguage)
        {
            return languageRepository.GetAll().Where(p => !p.IsDeleted && p.NameOfLanguage.Contains(nameOfLanguage)).ToList();
        }
    }
}

