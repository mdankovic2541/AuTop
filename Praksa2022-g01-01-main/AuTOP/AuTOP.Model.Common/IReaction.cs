using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuTOP.Model.Common
{
    public interface IReaction
    {
        Guid UserId { get; set; }

        Guid ReviewId { get; set; }

        bool IsLiked { get; set; } 

    }
}
