using System;
using System.Linq;

namespace Entities
{
    /// <summary>
    /// User's entity.
    /// </summary>
    public class User
    {
        public Guid UserId { get;  set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Info { get; set; }
        public bool IsApproved { get; set; }
        public Roles Role { get; set; }
        public DateTime CreateDate { get; set; }
        public IQueryable<Presentation> Presentations { get; set; }
        public IQueryable<Comment> Comments { get; set; }
        public IQueryable<Like> Likes { get; set; }

    }
}
