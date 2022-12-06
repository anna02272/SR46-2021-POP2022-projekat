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
    class LessonRepository : ILessonRepository
    {
        private static List<Lesson> lessons = new List<Lesson>();

        public void Add(Lesson lesson)
        {
            Data.Instance.Lessons.Add(lesson);
            Data.Instance.Save();
        }

        public void Add(List<Lesson> newLessons)
        {
            Data.Instance.Lessons.AddRange(newLessons);
            Data.Instance.Save();
        }

        public void Set(List<Lesson> newLessons)
        {
            Data.Instance.Lessons = newLessons;
        }

        public void Delete(int id)
        {
           Lesson lesson = GetById(id);

            if (lesson != null)
            {
                lesson.IsDeleted = true;
            }

            Data.Instance.Save();
        }

        public List<Lesson> GetAll()
        {
            return Data.Instance.Lessons;
        }

        public Lesson GetById(int id)
        {
            return Data.Instance.Lessons.Find(u => u.Id == id);
        }

        public void Update(int id, Lesson updatedLesson)
        {
            Data.Instance.Save();
        }

        //public void Save()
        //{
            //IFormatter formatter = new BinaryFormatter();
            //using (Stream stream = new FileStream(Config.lessonsFilePath, FileMode.Create, FileAccess.Write))
            //{
            //    formatter.Serialize(stream, lessons);
            //}
        //}
    }
}