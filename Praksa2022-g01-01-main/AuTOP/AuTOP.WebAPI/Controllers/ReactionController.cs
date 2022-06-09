using AuTOP.Model;
using AuTOP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuTOP.WebAPI.Controllers
{
    public class ReactionController : ApiController
    {
        public ReactionController(IReactionService reactionService)
        {
            this.ReactionService = reactionService;
        }
        protected IReactionService ReactionService { get; set; }


        public async Task<HttpResponseMessage> PostAsync(Reaction reaction)
        {           
            return Request.CreateResponse(HttpStatusCode.OK, await ReactionService.PostAsync(reaction));
        }

        public async Task<HttpResponseMessage> PutAsync(Reaction reaction)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await ReactionService.PutAsync(reaction)); 
        }

        public async Task<HttpResponseMessage> DeleteAsync(Guid userId, Guid reviewId)
        {          
            return Request.CreateResponse(HttpStatusCode.OK, await ReactionService.DeleteAsync(userId,reviewId));                   
        }
    }
}
