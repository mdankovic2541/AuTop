using AuTOP.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model
{
    public class Reaction : IdDateBaseModel, IReaction
    {
        public Guid UserId { get; set; }

        public Guid ReviewId { get; set; }

        public bool IsLiked { get; set; }
    }
}
