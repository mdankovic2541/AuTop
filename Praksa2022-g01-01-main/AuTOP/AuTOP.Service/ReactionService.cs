using AuTOP.Model;
using AuTOP.Repository.Common;
using AuTOP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Service
{
    public class ReactionService : IReactionService
    {

        public ReactionService(IReactionRepository reactionRepository)
        {
            this.ReactionRepository = reactionRepository;
        }
        protected IReactionRepository ReactionRepository { get; set; }

        public async Task<bool> PostAsync(Reaction reaction)
        {
            reaction.DateCreated = DateTime.UtcNow;
            reaction.DateUpdated = DateTime.UtcNow;
            return await ReactionRepository.PostAsync(reaction);
        }

        public async Task<bool> PutAsync(Reaction reaction)
        {
            reaction.DateUpdated = DateTime.UtcNow;
            return await ReactionRepository.PutAsync(reaction);
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid reviewId)
        {
            return await ReactionRepository.DeleteAsync(userId, reviewId);
        }

       
}
}
