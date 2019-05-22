using ERP.DAL;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Service
{
    public class UserService
    {
        private IRepository<User> userRepository = null;
        public bool isSave;
        public UserService()
        {
            userRepository = new EFRepositoryBase<User>();
        }
        public List<User> GetUsers()
        {
            return userRepository.Entities.ToList();
        }
        public async Task<int> Add()
        {
            try
            {
                return await userRepository.InsertAsync(new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "LJY",
                    Password = "123",
                    UserName = "李军毅",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }

        public int Add(User user)
        {
            return userRepository.Insert(user, isSave);
        }

        public int Update(User user)
        {
            return userRepository.Update(user, isSave);
        }

        public User GetUser()
        {
            return userRepository.Entities.FirstOrDefault();
        }
    }
}
