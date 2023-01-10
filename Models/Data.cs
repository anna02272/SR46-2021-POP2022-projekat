using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SR46_2021_POP2022.Models
{
	[Serializable]
	public class Data
	{
		[NonSerialized]
		private static Data instance;

		public List<User> Users { get; set; }

		public List<Professor> Professors { get; set; }

		public List<Student> Students { get; set; }

		public List<School> Schools { get; set; }

		public List<Language> Languages { get; set; }

		public List<Lesson> Lessons { get; set; }

		public List<Address> Addresses { get; set; }

		public List<Administrator> Administrators { get; set; }

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
			private set
			{
				instance = value;
			}
		}

		static Data()
		{
		}

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

		public void Initialize()
		{
			Address address = new Address
			{
				Id = 1,
				City = "Novi Sad",
				Country = "Srbija",
				Street = "ulica1",
				StreetNumber = "22",
				IsDeleted = false
			};
			User user1 = new User
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
			Professor professor = new Professor
			{
				User = user2
			};
			Professors.Add(professor);
			Student student = new Student
			{
				User = user3
			};
			Students.Add(student);
		}

		public void Save()
		{
			IFormatter formatter = new BinaryFormatter();
			//using Stream stream = new FileStream(Config.dataFilePath, FileMode.Create, FileAccess.Write);
			//formatter.Serialize(stream, this);
		}

		public static void Load()
		{
			try
			{
				IFormatter formatter = new BinaryFormatter();
				//using Stream stream = new FileStream(Config.dataFilePath, FileMode.Open, FileAccess.Read);
				//Instance = (Data)formatter.Deserialize(stream);
			}
			catch (Exception)
			{
				Instance = new Data();
			}
		}
	}
}
