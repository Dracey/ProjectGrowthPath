using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.DTOs
{
    public class SetupStateDto
    {
        public UserProfile NewUser { get; init; } = new UserProfile();
        public string AvatarStyle { get; set; } = "avataaar";
        public string? SelectedAvatarSeed { get; set; }
        public List<AvatarInfoDto> GeneratedAvatars { get; set; } = new();
        public Dictionary<int, Competence> SelectedInterests { get; set; } = new();
        public Dictionary<int, Competence> SelectedSkills { get; set; } = new();
        public Competence? ChosenCompetence { get; set; }
        public List<int> SelectedTools { get; set; } = new();
        public DateTime? TargetDate { get; set; }
    }
}
