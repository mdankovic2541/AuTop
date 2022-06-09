using AuTOP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Service.Common
{
    public interface IReactionService
    {

        Task<bool> PostAsync(Reaction reaction);

        Task<bool> PutAsync(Reaction reaction);
        
        Task<bool> DeleteAsync(Guid userId, Guid reviewId);
        
    }
}
