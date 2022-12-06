using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using SR46_2021_POP2022.Models;
using SR46_2021_POP2022;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class Data
    {
        [NonSerialized]
        private static Data instance;

       
        public List<User> Users { get; set; }
        public List<Professor> Professors { get; set; }
        public  List <Student> Students { get; set; }
        public List<School> Schools { get; set; }
        public List<Language> Languages { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Administrator> Administrators { get; set; }
       





        static Data() { }

        private Data()
        {
           
            Users = new List<User>();
            Professors = new List<Professor>();
            Students = new List<Student>();
            Schools = new List<School>();
            Languages = new List<Language>();
            Lessons = new List<Lesson>();
            Addresses = new List<Address>();
            Administrators = new List<Administrator>();
          
        }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }

                return instance;
            }

            private set => instance = value;
        }

  

        public void Initialize()
        {
            Address address = new Address
            {
                Id = 1,
                City = "Novi Sad",
                Country = "Srbija",
                Street = "ulica1",
                StreetNumber = "22",
                IsDeleted = false,
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
                Email = "ana@gmail.com",
                FirstName = "ana",
                LastName = "Anic",
                JMBG = "121346789012",
                Password = "ana",
                Gender = EGender.FEMEALE,
                UserType = EUserType.STUDENT,
                IsActive = true,
                Address = address
            };

            Users.Add(user1);
            Addresses.Add(address);

            var professor = new Professor
            {
                User = user2
            };

            Professors.Add(professor);

            var student = new Student
            {
                User = user3
            };

            Students.Add(student);
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.dataFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, this);
            }
        }

        public static void Load()
        {
           
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(Config.dataFilePath, FileMode.Open, FileAccess.Read))
                {
                    Instance = (Data)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Instance = new Data();
            }

        }
    }
}

//    sealed class Data
//    {

//        private static readonly Data instance = new Data();
//        public IUserService UserService { get; set; }
//        public IProfessorService ProfessorService { get; set; }
//        public IStudentService StudentService { get; set; }
//        public IAddressService AddressService { get; set; }
//        public ISchoolService SchoolService { get; set; }
//        public ILanguageService LanguageService { get; set; }
//        public ILessonService LessonService { get; set; }



//        static Data() { }

//        private Data()
//        {
//            UserService = new UserService();
//            ProfessorService = new ProfessorService();
//            StudentService = new StudentService();
//            AddressService = new AddressService();
//            SchoolService = new SchoolService();
//            LanguageService = new LanguageService();
//            LessonService = new LessonService();
//        }

//        public static Data Instance
//        {
//            get
//            {
//                return instance;
//            }
//        }


//        public void Initialize()
//        {
//            Address address = new Address
//            {
//                City = "Novi Sad",
//                Country = "Srbija",
//                Street = "ulica1",
//                StreetNumber = "22",
//                Id = 1
//            };

//            User user1 = new User()
//            {
//                FirstName = "Pera",
//                LastName = "Peric",
//                Email = "pera@gmail.com",
//                JMBG = "121346",
//                Password = "peki",
//                Gender = EGender.MALE,
//                Address = address,
//                UserType = EUserType.ADMINISTRATOR,
//                IsActive = true
//            };

//            User user2 = new User
//            {
//                Email = "mika@gmail.com",
//                FirstName = "mika",
//                LastName = "Mikic",
//                JMBG = "121346",
//                Password = "zika",
//                Gender = EGender.FEMEALE,
//                UserType = EUserType.PROFESSOR,
//                IsActive = true,
//                Address = address
//            };

//            User user3 = new User
//            {
//                Email = "anastasija@gmail.com",
//                FirstName = "anast",
//                LastName = "cukeli",
//                JMBG = "123456789124",
//                Password = "12345678",
//                Gender = EGender.FEMEALE,
//                UserType = EUserType.STUDENT,
//                IsActive = true,
//                Address = address
//            };

//            UserService.Add(user1);
//            ProfessorService.Add(user2);
//            StudentService.Add(user3);
//            AddressService.Add(address);

//        }

//        public void LoadData()

//        {
//            var users = LoadUsers();
//            var professors = LoadProfessors();
//            var students = LoadStudents();
//            var addresses = LoadAddresses();
//            var languages = LoadLanguages();
//            var schools = LoadSchools();
//            var lessons = LoadLessons();



//            foreach (var professor in professors)
//            {
//                var user = users.Find(u => u.Email == professor.UserId);
//                professor.User = user;

//            }
//            foreach (var student in students)
//            {
//                var user = users.Find(u => u.Email == student.UserId);
//                student.User = user;
//            }
//            foreach (var address in addresses)
//            {


//            }
//            foreach (var school in schools)
//            {

//            }
//            foreach (var language in languages)
//            {

//            }
//            foreach (var lesson in lessons)
//            {

//            }

//            UserService.Set(users);
//            ProfessorService.Set(professors);
//            StudentService.Set(students);
//            AddressService.Set(addresses);
//            LanguageService.Set(languages);
//            SchoolService.Set(schools);
//            LessonService.Set(lessons);
//        }



//        private List<User> LoadUsers()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();

//                using (Stream stream = new FileStream(Config.usersFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<User>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<User>();
//            }
//        }

//        private List<Professor> LoadProfessors()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<Professor>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<Professor>();
//            }

//        }

//        private List<Student> LoadStudents()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.studentsFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<Student>)formatter.Deserialize(stream);
//                     }
//                }
//            catch (Exception e)
//            {
//                return new List<Student>();
//            }

//        }
//        private List<Address> LoadAddresses()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.addressesFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<Address>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<Address>();
//            }

//        }
//        private List<School> LoadSchools()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.schoolsFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<School>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<School>();
//            }

//        }
//        private List<Language> LoadLanguages()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.languagesFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<Language>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<Language>();
//            }

//        }
//        private List<Lesson> LoadLessons()
//        {
//            try
//            {
//                IFormatter formatter = new BinaryFormatter();
//                using (Stream stream = new FileStream(Config.lessonsFilePath, FileMode.Open, FileAccess.Read))
//                {
//                    return (List<Lesson>)formatter.Deserialize(stream);
//                }
//            }
//            catch (Exception e)
//            {
//                return new List<Lesson>();
//            }

//        }
//    }
//}

