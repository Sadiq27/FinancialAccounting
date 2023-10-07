using ProjectORM.Context;
using ProjectORM.Models;
using ProjectORM.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectORM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User GetUserByUsername(string username)
        {   
            return dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public User GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
