using AuTOP.Common;
using AuTOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository.Common
{
    public interface ITransmissionRepository
    {
        Task<List<Transmission>> GetAllAsync(TransmissionFilter filter, Sorting sort, Paging paging);

        Task<Transmission> GetByIdAsync(Guid id);
    }
}
