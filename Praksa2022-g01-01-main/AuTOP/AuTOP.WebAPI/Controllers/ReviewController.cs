using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AuTOP.Common;
using AuTOP.Model;
using AuTOP.Model.Common;
using AuTOP.Service;
using AuTOP.Service.Common;
using AuTOP.WebAPI.Models.ViewModels;

namespace AuTOP.WebAPI.Controllers
{
    public class ReviewController : ApiController
    {
        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            this.ReviewService = reviewService;
            this.mapper = mapper;
        }
        protected IReviewService ReviewService { get; set; }
        private IMapper mapper;

        [Route("reviews")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] ReviewFilter filter, [FromUri] Sorting sort, [FromUri] Paging paging)
        {
            var reviews = await ReviewService.GetAsync(filter, sort, paging);
            List<ReviewViewModel> reviewsView = new List<ReviewViewModel>();

            if (reviews != null)
            {
                foreach (Review r in reviews)
                {
                    ReviewViewModel review = mapper.Map<Review, ReviewViewModel>(r);
                    review.Id = r.Id;
                    review.User.Id = r.UserId;
                    //review.ModelVersion.Id = r.ModelVersionId;                    
                    review.DateCreated = r.DateCreated;
                    reviewsView.Add(review);
                }
                return Request.CreateResponse(HttpStatusCode.OK, reviewsView);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("reviews/{reviewId}")]
        public async Task<HttpResponseMessage> GetByIdAsync(Guid reviewId)
        {
            Review review = await ReviewService.GetByIdAsync(reviewId);
            ReviewViewModel reviewView = new ReviewViewModel();

            if(review != null)
            {
                reviewView = mapper.Map<Review, ReviewViewModel>(review);
                reviewView.DateCreated = review.DateCreated;
                return Request.CreateResponse(HttpStatusCode.OK, reviewView);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        //[Authorize]
        [Route("reviews")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] Review review)
        {
            Review reviewPost = review;

            if(await ReviewService.PostAsync(reviewPost))
            {                
                return Request.CreateResponse(HttpStatusCode.OK, "New review created");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        //[Authorize]
        [Route("reviews/{id}")]
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] Review review)
        {
            Review reviewPut = review;
            
            if(await ReviewService.PutAsync(id, reviewPut))
            {                
                return Request.CreateResponse(HttpStatusCode.OK, "Review updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        //[Authorize]
        [Route("reviews/{id}")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if(await ReviewService.DeleteAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Review deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Review with Id:{id} not found");
            }
        }
    }
}
