using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    class LessonService : ILessonService
    {
        private ILessonRepository lessonRepository;
       
        public LessonService()
        {
            lessonRepository = new LessonRepository();
           
        }

        public Lesson GetById(int id)
        {
            return lessonRepository.GetById(id);
        }

        public List<Lesson> GetAll()
        {
            return lessonRepository.GetAll();
        }

        public List<Lesson> GetAvailableLessons()
        {
            return lessonRepository.GetAll().Where(p => !p.IsDeleted).ToList();
        }

       

        public void Add(Lesson lesson)
        {
            lessonRepository.Add(lesson);

        }

        public void Set(List<Lesson> lessons)
        {
            lessonRepository.Set(lessons);
        }

        public void Update(int id, Lesson lesson)
        {
            lessonRepository.Update(id, lesson);
         
        }

        public void Delete(int id)
        {
           
            lessonRepository.Delete(id);
        }

        public List<Lesson> ListAllLessons()
        {
            throw new NotImplementedException();
        }

        public List<Lesson> GetAvailableLessonsByName(string name)
        {
            return lessonRepository.GetAll().Where(p => !p.IsDeleted && p.Name.Contains(name)).ToList();
        }

        public List<Lesson> GetAvailableLessonsOrderedByName()
        {
            return lessonRepository.GetAll().Where(p => !p.IsDeleted).OrderBy(p => p.Name).ToList();
        }
    }
}

