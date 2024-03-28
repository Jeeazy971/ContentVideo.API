using ContentVideo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentVideo.Data
{
    public class ContentVideoDbContext : DbContext
    {
        public ContentVideoDbContext(DbContextOptions<ContentVideoDbContext> dbContext) : base(dbContext)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
