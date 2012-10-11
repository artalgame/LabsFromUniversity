using System.Data.Entity;
using Entities;

namespace SimPressDomainModel.EFDbContext
{
    /// <summary>
    /// ORM class which connect to database and represent data to classes
    /// </summary>
    public class SimPressEFDBContext:DbContext
    {
        public SimPressEFDBContext(string connectionString)
            : base(connectionString)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet Roles { get; set; }
    }
}
