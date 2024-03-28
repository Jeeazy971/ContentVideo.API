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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Title = "Admin", Description = "Accès complet à toutes les fonctionnalités." },
                new Role { Id = Guid.NewGuid(), Title = "User", Description = "Accès de base pour interagir avec l'application." },
                new Role { Id = Guid.NewGuid(), Title = "Guest", Description = "Accès limité, principalement en lecture seule." },
                new Role { Id = Guid.NewGuid(), Title = "Developer", Description = "Accès aux API et aux fonctionnalités de développement." }
            );
        }

    }
}
