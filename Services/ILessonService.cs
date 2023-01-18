using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface ILessonService
    {
        List<Lesson> GetAll();
        Lesson GetById(int id);
        List<Lesson> GetAvailableLessons();
        List<Lesson> GetAvailableLessonsByName(string name);
        List<Lesson> GetAvailableLessonsOrderedByName();
       
        void Add(Lesson lesson);
        void Set(List<Lesson> lessons);
        void Update(int id,Lesson lesson);
        void Delete(int id);
        
    }
}
