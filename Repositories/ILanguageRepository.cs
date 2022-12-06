using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface ILanguageRepository
    {

        List<Language> GetAll();
        Language GetById(int id);
        void Add(Language language);
        void Add(List<Language> languages);
        void Set(List<Language> languages);
        void Update(int id, Language language);
        void Delete(int id);


    }
}
