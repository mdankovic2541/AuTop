using AuTOP.Common;
using AuTOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Repository.Common
{
    public interface IBodyShapeRepository
    {
        Task<List<BodyShape>> GetAllAsync(BodyShapeFilter filter, Sorting sort, Paging paging);

        Task<BodyShape> GetByIdAsync(Guid id);
    }
}
