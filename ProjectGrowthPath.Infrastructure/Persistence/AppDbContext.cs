using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Domain.Entities;


namespace ProjectGrowthPath.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;

            if (Database.CanConnect())
            {
                _logger.LogInformation("Databaseverbinding succesvol!");
            }
            else
            {
                _logger.LogError("Databaseverbinding mislukt! ");
            }
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional configuration here
        }
    }
   
}
