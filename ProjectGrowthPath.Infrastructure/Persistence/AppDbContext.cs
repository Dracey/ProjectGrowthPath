using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.Enums.Competences;


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
        public DbSet<LearningToolCompetence> ToolCompetences { get; set; }

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

            // Configure Competence
            modelBuilder.Entity<Competence>()
                .Property(c => c.Category)
                .HasConversion<string>();

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

            // Configure LearningToolCompetence
            modelBuilder.Entity<LearningToolCompetence>()
                .HasKey(tc => tc.LearningToolCompID);
            modelBuilder.Entity<LearningToolCompetence>()
                .HasOne(tc => tc.LearningTool)
                .WithMany(t => t.ToolCompetences)
                .HasForeignKey(tc => tc.LearningToolID);

            modelBuilder.Entity<LearningToolCompetence>()
                .HasOne(tc => tc.Competence)
                .WithMany()
                .HasForeignKey(tc => tc.CompetenceID);

            

            modelBuilder.Entity<Competence>().HasData(
                new Competence
                {
                    CompetenceID = 1,
                    Name = "Samenwerken",
                    Description = "Effectief samenwerken met collega’s en teams.",
                    Category = CompetenceCategory.SoftSkill

                },
                new Competence
                {
                    CompetenceID = 2,
                    Name = "Communicatie",
                    Description = "Duidelijk en effectief communiceren, zowel schriftelijk als mondeling.",
                    Category = CompetenceCategory.SoftSkill
                },
                new Competence
                {
                    CompetenceID = 3,
                    Name = "Probleemoplossend vermogen",
                    Description = "In staat zijn om gestructureerd problemen te analyseren en op te lossen.",
                    Category = CompetenceCategory.SoftSkill
                },
                new Competence
                {
                    CompetenceID = 4,
                    Name = "Organiseren",
                    Description = "Plannen, structureren en prioriteiten stellen in taken en projecten.",
                    Category = CompetenceCategory.SoftSkill
                },
                new Competence
                {
                    CompetenceID = 5,
                    Name = "C# Ontwikkeling",
                    Description = "Schrijven en onderhouden van softwareapplicaties met C# en .NET.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 6,
                    Name = "Databasebeheer",
                    Description = "Werken met relationele databases, SQL en datamodellering.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 7,
                    Name = "Security awareness",
                    Description = "Begrijpen van beveiligingsprincipes, zoals authenticatie, autorisatie en veilige codering.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 8,
                    Name = "Cloud computing",
                    Description = "Basiskennis van cloudplatformen zoals Azure of AWS.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 9,
                    Name = "Softwarearchitectuur",
                    Description = "Ontwerpen van schaalbare, onderhoudbare software volgens design patterns en principes.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 10,
                    Name = "Leiderschap",
                    Description = "Anderen aansturen, inspireren en richting geven aan een team of project.",
                    Category = CompetenceCategory.SoftSkill
                },
                new Competence
                {
                    CompetenceID = 11,
                    Name = "Scrum",
                    Description = "Kennis van de Scrum-methodiek en ervaring met werken in Agile teams.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 12,
                    Name = "Projectmatig werken",
                    Description = "Het systematisch plannen, uitvoeren en opleveren van projecten.",
                    Category = CompetenceCategory.SoftSkill
                },
                new Competence
                {
                    CompetenceID = 13,
                    Name = "Clean Architecture",
                    Description = "Het ontwerpen en structureren van software volgens Clean Architecture principes.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 14,
                    Name = "Blazor ontwikkeling",
                    Description = "Kennis van het bouwen van interactieve webapplicaties met Blazor WebAssembly.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 15,
                    Name = "DevOps basiskennis",
                    Description = "Inzicht in CI/CD, automatisering van deployments en samenwerking tussen Dev en Ops.",
                    Category = CompetenceCategory.HardSkill
                },
                new Competence
                {
                    CompetenceID = 16,
                    Name = "Git & versiebeheer",
                    Description = "Versiebeheer beheersen met Git, inclusief branching, commits en pull requests.",
                    Category = CompetenceCategory.HardSkill
                }
                );
        }
    }
   
}
