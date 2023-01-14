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
    class LessonRepository : ILessonRepository
    {

        public int Add(Lesson lesson)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Lessons (Name, ProfessorId, Date,  Duration, Status, StudentId, IsDeleted)
                    output inserted.Id
                    values (@Name, @ProfessorId, @Date, @Duration, @Status, @StudentId, @IsDeleted)"
                ;

                command.Parameters.Add(new SqlParameter("Name", lesson.Name));
                command.Parameters.Add(new SqlParameter("ProfessorId", (object)lesson.ProfessorId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Date", lesson.Date));
                //command.Parameters.Add(new SqlParameter("Time", lesson.Time));
                command.Parameters.Add(new SqlParameter("Duration", lesson.Duration));
                command.Parameters.Add(new SqlParameter("Status", lesson.Status));
                command.Parameters.Add(new SqlParameter("StudentId", (object)lesson.StudentId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("IsDeleted", lesson.IsDeleted));

                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<Lesson> lessons)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Lessons set IsDeleted=1 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Lesson> GetAll()
        {
            List<Lesson> lessons = new List<Lesson>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Lessons";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Lessons");

                foreach (DataRow row in ds.Tables["Lessons"].Rows)
                {
                    var lesson = new Lesson
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        ProfessorId = (int)row["ProfessorId"],
                        Date = (DateTime)row["Date"] ,
                        //Time = (DateTime)row["Time"],
                        Duration = (TimeSpan)row["Duration"],
                        Status = (bool)row["Status"],
                        StudentId = (int)row["StudentId"],

                        IsDeleted = (bool)row["IsDeleted"]

                    };

                    lessons.Add(lesson);
                }
            }

            return lessons;
        }

        public Lesson GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Lessons where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Lessons");
                if (ds.Tables["Lessons"].Rows.Count > 0)
                {
                    var row = ds.Tables["Lessons"].Rows[0];

                    var lesson = new Lesson
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        ProfessorId = (int)row["ProfessorId"],
                        Date = (DateTime)row["Date"],
                        //Time = (DateTime)row["Time"],
                        Duration = (TimeSpan)row["Duration"],
                        Status = (bool)row["Status"],
                        StudentId = (int)row["StudentId"],

                        IsDeleted = (bool)row["IsDeleted"]
                    };

                    return lesson;
                }
            }

            return null;
        }

        public void Set(List<Lesson> lessons)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Lesson lesson)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Lessons set 
                        Name = @Name,
                        ProfessorId = @ProfessorId,
                        Date = @Date,
                        Duration = @Duration,
                        Status = @Status,
                        StudentId = @StudentId
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("Name", lesson.Name));
                command.Parameters.Add(new SqlParameter("Date", lesson.Date));
                //command.Parameters.Add(new SqlParameter("Time", lesson.Time));
                command.Parameters.Add(new SqlParameter("Duration", lesson.Duration));
                command.Parameters.Add(new SqlParameter("Status", lesson.Status));


                command.ExecuteScalar();
            }
        }
    }
    }

        //private static List<Lesson> lessons = new List<Lesson>();

        //public void Add(Lesson lesson)
        //{
        //    Data.Instance.Lessons.Add(lesson);
        //    Data.Instance.Save();
        //}

        //public void Add(List<Lesson> newLessons)
        //{
        //    Data.Instance.Lessons.AddRange(newLessons);
        //    Data.Instance.Save();
        //}

        //public void Set(List<Lesson> newLessons)
        //{
        //    Data.Instance.Lessons = newLessons;
        //}

        //public void Delete(string id)
        //{
        //   Lesson lesson = GetById(id);

        //    if (lesson != null)
        //    {
        //        lesson.IsDeleted = true;
        //    }

        //    Data.Instance.Save();
        //}

        //public List<Lesson> GetAll()
        //{
        //    return Data.Instance.Lessons;
        //}

        //public Lesson GetById(string id)
        //{
        //    return Data.Instance.Lessons.Find(u => u.Id == id);
        //}

        //public void Update(string id, Lesson updatedLesson)
        //{
        //    Data.Instance.Save();
        //}

        //public void Save()
        //{
        //IFormatter formatter = new BinaryFormatter();
        //using (Stream stream = new FileStream(Config.lessonsFilePath, FileMode.Create, FileAccess.Write))
        //{
        //    formatter.Serialize(stream, lessons);
        //}
        //}
    