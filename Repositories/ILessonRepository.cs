using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Repositories
{
    interface ILessonRepository
    {
        List<Lesson> GetAll();
        Lesson GetById(int id);
        void Add(Lesson lesson);
        void Add(List<Lesson> lesson);
        void Set(List<Lesson> lesson);
        void Update(int id, Lesson lesson);
        void Delete(int id);

    }
}
