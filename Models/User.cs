using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Models
{
    [Serializable]
    public class User : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public EGender Gender { get; set; }
        public EUserType UserType { get; set; }

        private Address address;
        public Address Address
        {
            get => address;
            set
            {
                address = value;
                AddressId = address?.Id;
            }
        }
        public int? AddressId { get; set; }

        public bool IsActive { get; set; }
        public bool IsValid { get; set; }

        public User()
        {
            IsActive = true;
        }

        public override string ToString()
        {
            return $"[User] {FirstName} {LastName}, {Email}";
        }

        public object Clone()
        {
            return new User
            {
                Id = Id,
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                JMBG = JMBG,
                UserType = UserType,
                Gender = Gender,
                IsActive = IsActive,
                Address = Address?.Clone() as Address
            };
        }
        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Email) )
                {
                    return "Email cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    return "Password cannot be empty!";
                }
                else if (string.IsNullOrEmpty(FirstName))
                {
                    return "First name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(LastName))
                {
                    return "Last name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(JMBG))
                {
                    return "JMBG cannot be empty!";
                }

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Email" && string.IsNullOrEmpty(Email))
                {
                    IsValid = false;
                    return "Email cannot be empty!";
                }
                else if (columnName == "Password" && string.IsNullOrEmpty(Password))
                {
                    IsValid = false;
                    return "Password cannot be empty!";
                }
                else if (columnName == "FirstName" && string.IsNullOrEmpty(FirstName))
                {
                    IsValid = false;
                    return "First name cannot be empty!";
                }
                else if (columnName == "LastName" && string.IsNullOrEmpty(LastName))
                {
                    IsValid = false;
                    return "Last name cannot be empty!";
                }
                else if (columnName == "JMBG" && string.IsNullOrEmpty(JMBG))
                {
                    IsValid = false;
                    return "JMBG cannot be empty!";
                }

                return "";
            }
        }
        public bool Login(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Users where Email='{email}' and Password='{password}'";
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
                    if (user.IsActive)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

    }
    //[Serializable]
    //class User
    //{
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string JMBG { get; set; }
    //    public EGender Gender { get; set; }
    //    public EUserType UserType { get; set; }
    //    public Address Address { get; set; }

    //    public bool IsActive { get; set; }


    //    public User() { }

    //    public override string ToString()
    //    {
    //        return $"[User] {FirstName} {LastName}, {Email}";
    //    }
    //}
}

