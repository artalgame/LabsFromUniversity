using System;
using System.Linq;

namespace Entities
{
    /// <summary>
    /// Presentation's entity.
    /// </summary>
    public class Presentation
    {
        public Guid PresentationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string SlidesJSON { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        public IQueryable<Comment> Comments { get; set; }
        public IQueryable<Like> Likes { get; set; }
    }
}
