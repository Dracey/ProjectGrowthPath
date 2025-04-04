using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Domain.Entities;


namespace ProjectGrowthPath.Infrastructure.Persistence
{
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional configuration here
        }
    }
   
}
