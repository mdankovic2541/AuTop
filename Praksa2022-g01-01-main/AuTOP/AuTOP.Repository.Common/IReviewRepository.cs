using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.Common;

namespace AuTOP.Repository.Common
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAsync(ReviewFilter filter, Sorting sort, Paging paging);
        Task<Review> GetByIdAsync(Guid reviewId);
        Task<bool> PostAsync(Review review);
        Task<bool> PutAsync(Guid reveiwId, Review review);
        Task<bool> DeleteAsync(Guid reviewId);
    }
}
