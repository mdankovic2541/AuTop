using AuTOP.Common;
using AuTOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository.Common
{
    public interface IMotorRepository
    {
        Task<List<Motor>> GetAllAsync(MotorFilter filter, Sorting sort, Paging paging);

        Task<Motor> GetByIdAsync(Guid id);

        Task<bool> PostAsync(Motor motor);

        Task<bool> PutAsync(Motor motor);

        Task<bool> DeleteAsync(Guid Id);
    }
}
