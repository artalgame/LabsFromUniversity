using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimPressDomainModel.Entities
{
    /// <summary>
    /// Entity of "like"
    /// </summary>
    public class Like
    {
        public Guid LikeId { get; set; }
        public Guid PresentationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        public Presentation Presentation { get; set; }
    }
}
