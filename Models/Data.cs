using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SR46_2021_POP2022.Services;
using System.Security.AccessControl;

namespace SR46_2021_POP2022.Models
{
    sealed class Data
    {
        
        private static readonly Data instance = new Data();
        public IUserService UserService { get; set; }
        public IProfessorService ProfessorService { get; set; }

        public IStudentService StudentService { get; set; }

        static Data() { }

        private Data()
        {
            UserService = new UserService();
            ProfessorService = new ProfessorService();
            StudentService = new StudentService();
        }

        public static Data Instance
        {
            get
            {
                return instance;
            }
        }


        public void Initialize()
        {
            Address address = new Address
            {
                City = "Novi Sad",
                Country = "Srbija",
                Street = "ulica1",
                StreetNumber = "22",
                Id = 1
            };

            User user1 = new User()
            {
                FirstName = "Pera",
                LastName = "Peric",
                Email = "pera@gmail.com",
                JMBG = "121346",
                Password = "peki",
                Gender = EGender.MALE,
                Address = address,
                UserType = EUserType.ADMINISTRATOR,
                IsActive = true
            };

            User user2 = new User
            {
                Email = "mika@gmail.com",
                FirstName = "mika",
                LastName = "Mikic",
                JMBG = "121346",
                Password = "zika",
                Gender = EGender.FEMEALE,
                UserType = EUserType.PROFESSOR,
                IsActive = true,
                Address = address
            };

            User user3 = new User
            {
                Email = "anastasija@gmail.com",
                FirstName = "anast",
                LastName = "cukeli",
                JMBG = "123456789124",
                Password = "12345678",
                Gender = EGender.FEMEALE,
                UserType = EUserType.STUDENT,
                IsActive = true,
                Address = address
            };

            UserService.Add(user1);
            ProfessorService.Add(user2);
            StudentService.Add(user3);
        }

        public void LoadData()
        {
            var users = LoadUsers();
            var professors = LoadProfessors();
            var students = LoadStudents();

            //if (professor)
            //{ 
                foreach (var professor in professors)
                {
                    var user = users.Find(u => u.Email == professor.UserId);
                    professor.User = user;
                }
            //} else { 
                foreach (var student in students)
                    {
                        var user = users.Find(u => u.Email == student.UserId);
                        student.User = user;
                    }
            //}
            UserService.Set(users);
            ProfessorService.Set(professors);
            StudentService.Set(students);
        }

        private List<User> LoadUsers()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();

                using (Stream stream = new FileStream(Config.usersFilePath, FileMode.Open, FileAccess.Read))
                {
                    return (List<User>)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                return new List<User>();
            }
        }

        private List<Professor> LoadProfessors()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Open, FileAccess.Read))
                {
                    return (List<Professor>)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                return new List<Professor>();
            }

        }

        private List<Student> LoadStudents()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Config.studentsFilePath, FileMode.Open, FileAccess.Read))
                {
                    return (List<Student>)formatter.Deserialize(stream);
                     }
                }
            catch (Exception e)
            {
                return new List<Student>();
            }

        }
    }
}

