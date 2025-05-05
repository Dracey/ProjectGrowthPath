using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectGrowthPath.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competences",
                columns: table => new
                {
                    CompetenceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competences", x => x.CompetenceID);
                });

            migrationBuilder.CreateTable(
                name: "LearningTools",
                columns: table => new
                {
                    LearningToolID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTools", x => x.LearningToolID);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ToolCompetences",
                columns: table => new
                {
                    LearningToolCompID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LearningToolID = table.Column<int>(type: "integer", nullable: false),
                    CompetenceID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolCompetences", x => x.LearningToolCompID);
                    table.ForeignKey(
                        name: "FK_ToolCompetences_Competences_CompetenceID",
                        column: x => x.CompetenceID,
                        principalTable: "Competences",
                        principalColumn: "CompetenceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolCompetences_LearningTools_LearningToolID",
                        column: x => x.LearningToolID,
                        principalTable: "LearningTools",
                        principalColumn: "LearningToolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalID);
                    table.ForeignKey(
                        name: "FK_Goals_UserProfiles_UserID",
                        column: x => x.UserID,
                        principalTable: "UserProfiles",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBadges",
                columns: table => new
                {
                    UserBadgeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    BadgeName = table.Column<string>(type: "text", nullable: false),
                    Received = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadges", x => x.UserBadgeID);
                    table.ForeignKey(
                        name: "FK_UserBadges_UserProfiles_UserID",
                        column: x => x.UserID,
                        principalTable: "UserProfiles",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompetences",
                columns: table => new
                {
                    UserCompID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetenceID = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompetences", x => x.UserCompID);
                    table.ForeignKey(
                        name: "FK_UserCompetences_Competences_CompetenceID",
                        column: x => x.CompetenceID,
                        principalTable: "Competences",
                        principalColumn: "CompetenceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompetences_UserProfiles_UserID",
                        column: x => x.UserID,
                        principalTable: "UserProfiles",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalLearningTools",
                columns: table => new
                {
                    GoalToolID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoalID = table.Column<int>(type: "integer", nullable: false),
                    LearningToolID = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalLearningTools", x => x.GoalToolID);
                    table.ForeignKey(
                        name: "FK_GoalLearningTools_Goals_GoalID",
                        column: x => x.GoalID,
                        principalTable: "Goals",
                        principalColumn: "GoalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalLearningTools_LearningTools_LearningToolID",
                        column: x => x.LearningToolID,
                        principalTable: "LearningTools",
                        principalColumn: "LearningToolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Competences",
                columns: new[] { "CompetenceID", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "SoftSkill", "Effectief samenwerken met collega’s en teams.", "Samenwerken" },
                    { 2, "SoftSkill", "Duidelijk en effectief communiceren, zowel schriftelijk als mondeling.", "Communicatie" },
                    { 3, "SoftSkill", "In staat zijn om gestructureerd problemen te analyseren en op te lossen.", "Probleemoplossend vermogen" },
                    { 4, "SoftSkill", "Plannen, structureren en prioriteiten stellen in taken en projecten.", "Organiseren" },
                    { 5, "HardSkill", "Schrijven en onderhouden van softwareapplicaties met C# en .NET.", "C# Ontwikkeling" },
                    { 6, "HardSkill", "Werken met relationele databases, SQL en datamodellering.", "Databasebeheer" },
                    { 7, "HardSkill", "Begrijpen van beveiligingsprincipes, zoals authenticatie, autorisatie en veilige codering.", "Security awareness" },
                    { 8, "HardSkill", "Basiskennis van cloudplatformen zoals Azure of AWS.", "Cloud computing" },
                    { 9, "HardSkill", "Ontwerpen van schaalbare, onderhoudbare software volgens design patterns en principes.", "Softwarearchitectuur" },
                    { 10, "SoftSkill", "Anderen aansturen, inspireren en richting geven aan een team of project.", "Leiderschap" },
                    { 11, "HardSkill", "Kennis van de Scrum-methodiek en ervaring met werken in Agile teams.", "Scrum" },
                    { 12, "SoftSkill", "Het systematisch plannen, uitvoeren en opleveren van projecten.", "Projectmatig werken" },
                    { 13, "HardSkill", "Het ontwerpen en structureren van software volgens Clean Architecture principes.", "Clean Architecture" },
                    { 14, "HardSkill", "Kennis van het bouwen van interactieve webapplicaties met Blazor WebAssembly.", "Blazor ontwikkeling" },
                    { 15, "HardSkill", "Inzicht in CI/CD, automatisering van deployments en samenwerking tussen Dev en Ops.", "DevOps basiskennis" },
                    { 16, "HardSkill", "Versiebeheer beheersen met Git, inclusief branching, commits en pull requests.", "Git & versiebeheer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalLearningTools_GoalID",
                table: "GoalLearningTools",
                column: "GoalID");

            migrationBuilder.CreateIndex(
                name: "IX_GoalLearningTools_LearningToolID",
                table: "GoalLearningTools",
                column: "LearningToolID");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserID",
                table: "Goals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ToolCompetences_CompetenceID",
                table: "ToolCompetences",
                column: "CompetenceID");

            migrationBuilder.CreateIndex(
                name: "IX_ToolCompetences_LearningToolID",
                table: "ToolCompetences",
                column: "LearningToolID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_UserID",
                table: "UserBadges",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetences_CompetenceID",
                table: "UserCompetences",
                column: "CompetenceID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompetences_UserID",
                table: "UserCompetences",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalLearningTools");

            migrationBuilder.DropTable(
                name: "ToolCompetences");

            migrationBuilder.DropTable(
                name: "UserBadges");

            migrationBuilder.DropTable(
                name: "UserCompetences");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "LearningTools");

            migrationBuilder.DropTable(
                name: "Competences");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
