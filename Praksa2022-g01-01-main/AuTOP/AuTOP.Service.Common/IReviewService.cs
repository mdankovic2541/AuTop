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
    public interface IReviewService
    {
        Task<List<Review>> GetAsync(ReviewFilter filter, Sorting sort, Paging paging);
        Task<Review> GetByIdAsync(Guid reviewId);
        Task<bool> PostAsync(Review review);
        Task<bool> PutAsync(Guid reviewId, Review review);
        Task<bool> DeleteAsync(Guid reviewId);
    }
}
