using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.Common;

namespace AuTOP.Service.Common
{

    public interface IUserService
    {
        Task<List<IUser>> GetAsync(UserFilter filter, Sorting sort, Paging paging);

        Task<IUser> GetByIdAsync(Guid userId);
        Task<bool> PostAsync(User user);
        Task<bool> PutAsync(Guid userId, User user);
        Task<bool> DeleteAsync(Guid userId);
        Task<Guid> GetIdbyName(string name);


    }
}
