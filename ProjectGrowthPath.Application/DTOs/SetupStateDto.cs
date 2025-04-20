using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.DTOs
{
    public class SetupStateDto
    {
        public UserProfile NewUser { get; init; } = new UserProfile();
        public List<Competence> Interests { get; set; } = new();
        public List<Competence> Skills { get; set; } = new();
        public Competence? ChosenCompetence { get; set; }
        public List<LearningTool> SelectedTools { get; set; } = new();
        public DateTime? TargetDate { get; set; }
    }
}
