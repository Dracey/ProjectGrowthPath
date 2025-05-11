using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Domain.ValueObjects
{
    public class SetupState
    {
        public UserProfile NewUser { get; init; } = new();

        public string AvatarStyle { get; set; } = "avataaars";
        public string? SelectedAvatarSeed { get; set; }

        public List<(string Seed, string Url)> GeneratedAvatars = new ();

        public Dictionary<int, Competence> SelectedInterests { get; set; } = new(); 
        public Dictionary<int, Competence> SelectedSkills { get; set; } = new();

        public List<int> SelectedTools { get; set; } = new();

        public Competence? ChosenCompetence { get; private set; }
        public DateTime? TargetDate { get; private set; }


        public void SetChosenCompetence(Competence competence)
        {
            ChosenCompetence = competence;
        }

        public void ToggleLearningTool(int toolId)
        {
            if (SelectedTools.Contains(toolId))
                SelectedTools.Remove(toolId);
            else
                SelectedTools.Add(toolId);
        }

        public void SetTargetDate(DateTime date)
        {
            if (date.Date < DateTime.Today)
                throw new ArgumentException("De doel-datum mag niet in het verleden liggen.");
            TargetDate = date.Date;
        }
    }
}