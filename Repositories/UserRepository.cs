using SR46_2021_POP2022.CustomExceptions;
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
using System.Data;

namespace SR46_2021_POP2022.Repositories
{
    class UserRepository : IUserRepository
    {
        public int Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Users (Email, Password, FirstName, LastName, Jmbg, Gender, UserType, IsActive, AddressId)
                    output inserted.Id
                    values (@Email, @Password, @FirstName, @LastName, @Jmbg, @Gender, @UserType, @IsActive, @AddressId)";

                command.Parameters.Add(new SqlParameter("Email", user.Email));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", user.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));
                command.Parameters.Add(new SqlParameter("AddressId", (object)user.AddressId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("IsActive", user.IsActive));

                return (int)command.ExecuteScalar();
            }
        }

        public void Add(List<User> users)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Users set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Users";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");

                foreach (DataRow row in ds.Tables["Users"].Rows)
                {
                    var user = new User
                    {
                        Id = (int)row["Id"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"]
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        public User GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Users where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");
                if (ds.Tables["Users"].Rows.Count > 0)
                {
                    var row = ds.Tables["Users"].Rows[0];

                    var user = new User
                    {
                        Id = (int)row["Id"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"]
                    };

                    return user;
                }
            }

            return null;
        }

        public void Set(List<User> users)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Users set 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Password = @Password,
                        Gender = @Gender,
                        UserType = @UserType
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));

                command.ExecuteScalar();
            }
        }
    }
}
//public UserRepository()
//{
//}

//public void Add(User user)
//{
//    Data.Instance.Users.Add(user);
//    Data.Instance.Save();
//}

//public void Add(List<User> newUsers)
//{
//    Data.Instance.Users.AddRange(newUsers);
//    Data.Instance.Save();
//}

//public void Set(List<User> newUsers)
//{
//    Data.Instance.Users = newUsers;
//}

//public void Delete(string email)
//{
//    User user = GetById(email);

//    if (user != null)
//    {
//        user.IsActive = false;
//    }
//    else
//    {
//        throw new UserNotFoundException();
//    }

//    Data.Instance.Save();
//}

//public List<User> GetAll()
//{
//    return Data.Instance.Users;
//}

//public User GetById(string email)
//{
//    return Data.Instance.Users.Find(u => u.Email == email);
//}

//public void Update(string email, User updatedUser)
//{
//    User user = GetById(email);

//    if (user != null)
//    {
//        user.Address = updatedUser.Address;
//        user.FirstName = updatedUser.FirstName;
//        user.LastName = updatedUser.LastName;
//        user.Password = updatedUser.Password;
//        user.Gender = updatedUser.Gender;
//        user.UserType = updatedUser.UserType;
//    }
//    Data.Instance.Save();
//}

//    class UserRepository : IUserRepository, IFilePersistence
//    {
//        private static List<User> users = new List<User>();

//        public UserRepository()
//        {
//        }

//        public void Add(User user)
//        {
//            users.Add(user);
//            Save();
//        }

//        public void Add(List<User> newUsers)
//        {
//            users.AddRange(newUsers);
//            Save();
//        }

//        public void Set(List<User> newUsers)
//        {
//            users = newUsers;
//        }

//        public void Delete(string email)
//        {
//            User user = GetById(email);

//            if (user != null)
//            {
//                user.IsActive = false;
//            }
//            else
//            {
//                throw new UserNotFoundException();
//            }

//            Save();
//        }

//        public List<User> GetAll()
//        {
//            return users;
//        }

//        public User GetById(string email)
//        {
//            return users.Find(u => u.Email == email);
//        }

//        public void Update(string email, User updatedUser)
//        {
//            User user = GetById(email);

//            if (user != null)
//            {
//                user.Address = updatedUser.Address;
//                user.FirstName = updatedUser.FirstName;
//                user.LastName = updatedUser.LastName;
//                user.UserType = updatedUser.UserType;
//            }
//            Save();
//        }

//        public void Save()
//        {
//            IFormatter formatter = new BinaryFormatter();
//            using (Stream stream = new FileStream(Config.usersFilePath, FileMode.Create, FileAccess.Write))
//            {
//                formatter.Serialize(stream, users);
//            }
//        }
//    }
//}
