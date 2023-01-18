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
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Xml.Linq;

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
        //public void Delete(int id)
        //{
        //    using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
        //    {
        //        conn.Open();

        //        SqlCommand command = conn.CreateCommand();
        //        command.CommandText = "SELECT Status FROM dbo.Lessons WHERE Id=@id";

        //        command.Parameters.Add(new SqlParameter("id", id));
        //        var status = command.ExecuteScalar();

        //        if ((bool)(status = false))
        //        {
        //            command.CommandText = "update dbo.Lessons set IsDeleted=1 where Id=@id";
        //            command.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            System.Windows.MessageBox.Show("Lesson is reserved , Cant be deleted.");

        //        }

        //    }
    


        //public List<Lesson> GetAll()
        //{
        //    List<Lesson> lessons = new List<Lesson>();

        //    using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
        //    {
        //        string commandText = "select * from dbo.Lessons";
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

        //        DataSet ds = new DataSet();

        //        dataAdapter.Fill(ds, "Lessons");

        //        foreach (DataRow row in ds.Tables["Lessons"].Rows)
        //        {
        //            var lesson = new Lesson
        //            {
        //                Id = (int)row["Id"],
        //                Name = row["Name"] as string,
        //                ProfessorId = (int)row["ProfessorId"],
        //                Date = (DateTime)row["Date"] ,
        //                //Time = (DateTime)row["Time"],
        //                Duration = (TimeSpan)row["Duration"],
        //                Status = (bool)row["Status"],
        //                StudentId = (int)row["StudentId"],

        //                IsDeleted = (bool)row["IsDeleted"]

        //            };

        //            lessons.Add(lesson);
        //        }
        //    }

        //    return lessons;
        //}



        public List<Lesson> GetAll()
        {
            List<Lesson> lessons = new List<Lesson>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "SELECT l.Id as LessonId, l.Name as LessonName, l.Date as LessonDate, l.Duration as LessonDuration, " +
                    "l.Status as LessonStatus, l.IsDeleted as LessonIsDeleted, u1.FirstName as ProfessorFirstName, u1.LastName as ProfessorLastName, u1.Email as ProfessorEmail, " +
                    "u2.FirstName as StudentFirstName, u2.LastName as StudentLastName, u2.Email as StudentEmail," +
                    " p.Id as ProfessorId, s.Id as StudentId FROM dbo.Lessons l JOIN dbo.Professors p ON l.ProfessorId = p.Id " +
                    "JOIN dbo.Users u1 ON p.UserId = u1.Id JOIN dbo.Students s ON l.StudentId = s.Id JOIN dbo.Users u2 ON s.UserId = u2.Id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Lessons");

                foreach (DataRow row in ds.Tables["Lessons"].Rows)
                {

                    var lesson = new Lesson
                    {
                        Id = (int)row["LessonId"],
                        Name = row["LessonName"] as string,
                        ProfessorId = (int)row["ProfessorId"],
                        Professor = new Professor
                        {
                            Id = (int)row["ProfessorId"],
                            User = new User
                            {
                                FirstName = row["ProfessorFirstName"] as string,
                                LastName = row["ProfessorLastName"] as string,
                                Email = row["ProfessorEmail"] as string

                            }
                        },
                        Date = (DateTime)row["LessonDate"],
                        Duration = (TimeSpan)row["LessonDuration"],
                        Status = (bool)row["LessonStatus"],
                        StudentId = (int)row["StudentId"],
                        Student = new Student
                        {
                            Id = (int)row["StudentId"],
                            User = new User
                            {
                                FirstName = row["StudentFirstName"] as string,
                                LastName = row["StudentLastName"] as string,
                                Email = row["StudentEmail"] as string

                            }
                        },
                         IsDeleted = (bool)row["LessonIsDeleted"]


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
                command.Parameters.Add(new SqlParameter("ProfessorId", lesson.ProfessorId));
                //command.Parameters.Add(new SqlParameter("Time", lesson.Time));
                command.Parameters.Add(new SqlParameter("Duration", lesson.Duration));
                command.Parameters.Add(new SqlParameter("StudentId", lesson.StudentId));
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
    