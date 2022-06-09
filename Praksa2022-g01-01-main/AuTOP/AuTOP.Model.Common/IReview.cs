using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.Common
{
    public interface IReview
    {
        Guid ModelVersionId { get; set; }
        Guid UserId { get; set; }
        string Comment { get; set; }
        int Rating { get; set; }
        double LikePercentage { get; set; }
        IReaction CurrentUserReaction { get; set; }
    }
}
