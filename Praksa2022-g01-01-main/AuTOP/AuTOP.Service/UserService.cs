using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.Repository;
using AuTOP.Repository.Common;
using AuTOP.Service.Common;

namespace AuTOP.Service
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }
        protected IUserRepository UserRepository { get; set; }
        public async Task<List<IUser>> GetAsync(UserFilter filter, Sorting sort, Paging paging)
        {
            return await UserRepository.GetAsync(filter, sort, paging);
        }

        public async Task<IUser> GetByIdAsync(Guid userId)
        {
            return await UserRepository.GetByIdAsync(userId);
        }

        public async Task<bool> PostAsync(User user)
        {
            user.Generate();
            return await UserRepository.PostAsync(user);
        }
        public async Task<bool> PutAsync(Guid userId, User user)
        {
            user.GenerateUpdateDate();
            return await UserRepository.PutAsync(userId, user);
        }
        public async Task<bool> DeleteAsync(Guid userId)
        {
            return await UserRepository.DeleteAsync(userId);
        }

        public async Task<Guid> GetIdbyName(string username)
        {
            return await UserRepository.GetIdbyName(username);
        }
    }
}
