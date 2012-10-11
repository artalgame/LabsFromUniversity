using System;

namespace Entities
{
    /// <summary>
    /// Comment's entity.
    /// </summary>
   public class Comment
    {
       public Guid CommentId { get; set; }
       public Guid PresentationId { get; set; }
       public string Text { get; set; }
       public DateTime CreateDate { get; set; }
       public User User { get; set; }
       public Presentation Presentation { get; set; }

    }
}
