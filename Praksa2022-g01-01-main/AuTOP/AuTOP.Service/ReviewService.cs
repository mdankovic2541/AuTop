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
    public class ReviewService : IReviewService
    {
        public ReviewService(IReviewRepository reviewRepository, IReactionRepository reactionRepository, IUserRepository userRepository, IModelVersionRepository modelVersionRepository)
        {
            this.ReviewRepository = reviewRepository;
            this.UserRepository = userRepository;
            this.ReactionRepository = reactionRepository;
            this.ModelVersionRepository = modelVersionRepository;
        }
       
        protected IReviewRepository ReviewRepository { get; set; }
        protected IUserRepository UserRepository { get; set; }
        protected IReactionRepository ReactionRepository { get; set; }
        protected IModelVersionRepository ModelVersionRepository { get; set; }
        public async Task<List<Review>> GetAsync(ReviewFilter filter, Sorting sort, Paging paging)
        {
            List<Review> reviews = await ReviewRepository.GetAsync(filter, sort, paging);
            foreach (Review review in reviews)
            {
                review.User = await UserRepository.GetByIdAsync(review.UserId);
                //review.ModelVersion = await ModelVersionRepository.GetModelVersionById(review.ModelVersionId);
                review.LikePercentage = await ReactionRepository.GetLikePercentage(review.Id);
            }
            return reviews;
        }
        public async Task<Review> GetByIdAsync(Guid reviewId)
        {
            return await ReviewRepository.GetByIdAsync(reviewId);
        }
        public async Task<bool> PutAsync(Guid reviewId, Review review)
        {
            review.GenerateUpdateDate();
            return await ReviewRepository.PutAsync(reviewId, review);
        }
        public async Task<bool> PostAsync(Review review)
        {
            review.Generate();
            return await ReviewRepository.PostAsync(review);
        }

        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            return await ReviewRepository.DeleteAsync(reviewId);
        }
    }
}
