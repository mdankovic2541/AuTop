using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuTOP.Model.Common;

namespace AuTOP.Model
{
    public class Review : IdDateBaseModel, IReview
    {
        public Guid ModelVersionId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int  Rating { get; set; }
        public double LikePercentage { get; set; }
        public IUser User { get; set; }
        //public IModelVersion ModelVersion { get; set; }
        public IReaction CurrentUserReaction { get; set; }

    }
}
