using SR46_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR46_2021_POP2022.Services
{
    interface IUserService
    {
        List<User> GetAll();
        List<User> GetActiveUsers();
        void Add(User user);
        void Set(List<User> users);
        void Update(int id, User user);
        void Delete(int id);
    }
}
