using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface ISchoolRepository
    {
        List<School> GetAll();
        School GetById(string id);
        void Add(School school);
        void Add(List<School> schools);
        void Set(List<School> schools);
        void Update(string id, School school);
        void Delete(string id);

    }
}
