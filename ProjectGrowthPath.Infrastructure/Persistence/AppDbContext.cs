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

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<LearningTool> LearningTools { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<UserCompetence> UserCompetences { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<GoalLearningTool> GoalLearningTools { get; set; }
        public DbSet<ToolCompetence> ToolCompetences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure UserProfile
            modelBuilder.Entity<UserProfile>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<UserProfile>()
                .Property(u => u.ApplicationUserId)
                .IsRequired();

            // Configure UserBadge
            modelBuilder.Entity<UserBadge>()
                .HasKey(b => b.UserBadgeID);
            modelBuilder.Entity<UserBadge>()
                .HasOne(b => b.User)
                .WithMany(u => u.Badges)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure UserCompetence
            modelBuilder.Entity<UserCompetence>()
                .HasKey(uc => uc.UserCompID);
            modelBuilder.Entity<UserCompetence>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Competences)
                .HasForeignKey(uc => uc.UserID);

            modelBuilder.Entity<UserCompetence>()
                .HasOne(uc => uc.Competence)
                .WithMany()
                .HasForeignKey(uc => uc.CompetenceID);

            modelBuilder.Entity<UserCompetence>()
                .Property(uc => uc.Type)
                .HasConversion<string>();

            // Configure Goal
            modelBuilder.Entity<Goal>()
                .HasKey(g => g.GoalID);
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.UserProfile)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserID);

            // Configure LearningTool
            modelBuilder.Entity<LearningTool>()
                .HasKey(t => t.LearningToolID);

            // Configure GoalLearningTool
            modelBuilder.Entity<GoalLearningTool>()
                .HasKey(gt => gt.GoalToolID);
            modelBuilder.Entity<GoalLearningTool>()
                .HasOne(gt => gt.Goal)
                .WithMany(g => g.GoalLearningTools)
                .HasForeignKey(gt => gt.GoalID);

            modelBuilder.Entity<GoalLearningTool>()
                .HasOne(gt => gt.LearningTool)
                .WithMany(t => t.GoalLearningTools)
                .HasForeignKey(gt => gt.LearningToolID);

            // Configure ToolCompetence
            modelBuilder.Entity<ToolCompetence>()
                .HasKey(tc => tc.ToolCompID);
            modelBuilder.Entity<ToolCompetence>()
                .HasOne(tc => tc.LearningTool)
                .WithMany(t => t.ToolCompetences)
                .HasForeignKey(tc => tc.LearningToolID);

            modelBuilder.Entity<ToolCompetence>()
                .HasOne(tc => tc.Competence)
                .WithMany()
                .HasForeignKey(tc => tc.CompetenceID);
        }
    }
   
}
