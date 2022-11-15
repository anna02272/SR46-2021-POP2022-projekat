using SR46_2021_POP2022.CustomExceptions;
using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace SR46_2021_POP2022
{
    class Program
    {
        public static void ShowProfessors()
        {
            var professors = Data.Instance.ProfessorService.GetActiveProfessors();
            ShowProfessors(professors);
        }
        public static void ShowStudents()
        {
            var students = Data.Instance.StudentService.GetActiveStudents();
            ShowStudents(students);
        }

        private static void ShowProfessors(List<Professor> professors)
        {
            foreach (Professor professor in professors)
            {
                Console.WriteLine(professor);
            }
        }
        private static void ShowStudents(List<Student> students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
        }

        public static void AddProfessor()
        {
            Address address = new Address
            {
                StreetNumber = "2",
                Country = "Drzava 2",
                City = "Grad 2",
                Street = "Ulica 2",
                Id = 1
            };

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter first name:");
            string fistName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter JMBG:");
            string jmbg = Console.ReadLine();

            Console.WriteLine("Enter gender:(MALE/FEMALE/OTHER)");
            Enum.TryParse(Console.ReadLine(), out EGender gender);

            User user = new User
            {
                Address = address,
                Email = email,
                FirstName = fistName,
                LastName = lastName,
                JMBG = jmbg,
                Password = password,
                Gender = gender,
                UserType = EUserType.PROFESSOR,
                IsActive = true
            };

            Data.Instance.ProfessorService.Add(user);

            Console.WriteLine("Professor added successfuly");

        }
        public static void AddStudent()
        {
            Address address = new Address
            {
                StreetNumber = "3",
                Country = "Drzava 3",
                City = "Grad 3",
                Street = "Ulica 3",
                Id = 2
            };
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter first name:");
            string fistName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter JMBG:");
            string jmbg = Console.ReadLine();

            Console.WriteLine("Enter gender:(MALE/FEMALE/OTHER)");
            Enum.TryParse(Console.ReadLine(), out EGender gender);

            User user = new User
            {
                Address = address,
                Email = email,
                FirstName = fistName,
                LastName = lastName,
                JMBG = jmbg,
                Password = password,
                Gender = gender,
                UserType = EUserType.STUDENT,
                IsActive = true
            };

            Data.Instance.StudentService.Add(user);

            Console.WriteLine("Student added successfuly");

        }


        public static void UpdateProfessor()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            var professor = Data.Instance.ProfessorService.GetById(email);

            if (professor != null)
            {
                Console.WriteLine("Enter FirstName:"); //moze se menjati koji god atribut
                string newFirstName = Console.ReadLine();

                professor.User.FirstName = newFirstName;

                Data.Instance.ProfessorService.Update(email, professor);

                Console.WriteLine("Professor updated successfuly.");
            }
            else
            {
                Console.WriteLine("There are no professor with the given email");
            }
        }
        public static void UpdateStudent()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            var student = Data.Instance.StudentService.GetById(email);

            if (student != null)
            {
                Console.WriteLine("Enter FirstName:");
                string newFirstName = Console.ReadLine();

                student.User.FirstName = newFirstName;

                Data.Instance.StudentService.Update(email, student);

                Console.WriteLine("Student updated successfuly.");
            }
            else
            {
                Console.WriteLine("There is no student with the given email");
            }
        }

        private static void DeleteProfessor()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            try
            {
                Data.Instance.ProfessorService.Delete(email);
            }
            catch (UserNotFoundException e)
            {
                Console.WriteLine("User with the given email doesn't exsist");
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            try
            {
                Data.Instance.StudentService.Delete(email);
            }
            catch (UserNotFoundException e)
            {
                Console.WriteLine("User with the given email doesn't exist");
            }
        }

        private static void SearchByEmail()
        {
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            var professors = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(email);
            ShowProfessors(professors);

            var students = Data.Instance.StudentService.GetActiveStudentsByEmail(email);
            ShowStudents(students);
        }



        private static void SortByEmail()
        {
            var professors = Data.Instance.ProfessorService.GetActiveProfessorsOrderedByEmail();
            ShowProfessors(professors);

            var students = Data.Instance.StudentService.GetActiveStudentsOrderedByEmail();
            ShowStudents(students);
        }

        static void Main(string[] args)
        {
            //inicijalno kreiranje podataka i punjenje fajlova

            //Data.Instance.Initialize();

            //citanje iz tekstualnih fajlova
            Data.Instance.LoadData();

            string option;

            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("Option 1: Prikaz svih profesora");
                Console.WriteLine("Option 2: Unos novog profesora");
                Console.WriteLine("Option 3: Modifikacija profesora");
                Console.WriteLine("Option 4: Brisanje profesora");


                Console.WriteLine("Option 5: Prikaz svih studenata");
                Console.WriteLine("Option 6: Unos novog studenta");
                Console.WriteLine("Option 7: Modifikacija studenta");
                Console.WriteLine("Option 8: Brisanje studenta");

                Console.WriteLine("Option 9: Pretraga po email adresi");
                Console.WriteLine("Option 10: Sortiranje po email adresi");


                Console.WriteLine("Option X: Kraj");
                Console.WriteLine("Option>>>");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowProfessors();

                        break;
                    case "2":
                        AddProfessor();

                        break;
                    case "3":
                        UpdateProfessor();

                        break;
                    case "4":
                        DeleteProfessor();
                        break;
                    case "5":
                        ShowStudents();
                        break;
                    case "6":
                        AddStudent();
                        break;
                    case "7":
                        UpdateStudent();
                        break;
                    case "8":
                        DeleteStudent();
                        break;
                    case "9":
                        SearchByEmail();
                        break;
                    case "10":
                        SortByEmail();
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("Molimo ponovite unos opcije!");
                        break;
                }

            } while (!option.Equals("X"));
        }
    }
}
