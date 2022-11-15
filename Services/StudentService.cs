﻿using SR46_2021_POP2022.Models;
using SR46_2021_POP2022.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        private IUserRepository userRepository;

        public StudentService()
        {
            studentRepository = new StudentRepository();
            userRepository = new UserRepository();
        }
        public Student GetById(string email)
        {
            return studentRepository.GetById(email);
        }

        public List<Student> GetAll()
        {
            return studentRepository.GetAll();
        }
        public List<Student> GetActiveStudents()
        {
            return studentRepository.GetAll().Where(s => s.User.IsActive).ToList();
        }
        public List<Student> GetActiveStudentsByEmail(string email)
        {
            return studentRepository.GetAll().Where(s => s.User.IsActive && s.User.Email.Contains(email)).ToList();
        }
        public List<Student> GetActiveStudentsOrderedByEmail()
        {
            return studentRepository.GetAll().Where(s => s.User.IsActive).OrderBy(s => s.User.Email).ToList();

        }
        public void Add(User user)
        {
            userRepository.Add(user);

            var student = new Student
            {
                User = user,
                UserId = user.Email
            };
            studentRepository.Add(student);
        }
        public void Set(List<Student> students)
        {
            studentRepository.Set(students);
        }
        public void Update(string email, Student student)
        {
            userRepository.Update(email, student.User);
            studentRepository.Update(email, student);
        }
        public void Delete(string email)
        {
            userRepository.Delete(email);
            studentRepository.Delete(email);
        }
        public List<User> ListAllProfessors()
        {
            throw new NotImplementedException();
        }
    }
}
