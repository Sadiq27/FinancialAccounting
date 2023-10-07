using ProjectORM.Models;
using System.Collections.Generic;

namespace ProjectORM.Repositories.Base
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User GetUserByUsernameAndPassword(string username, string password);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
