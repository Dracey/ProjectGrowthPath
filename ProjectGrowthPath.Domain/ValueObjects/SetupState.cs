using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Domain.ValueObjects
{
    public class SetupState
    {
        public UserProfile NewUser { get; init; } = new();

        public string AvatarStyle { get; set; } = "avataaars";
        public string? SelectedAvatarSeed { get; set; }

        public List<(string Seed, string Url)> GeneratedAvatars = new ();

        private readonly List<Competence> _interests = new();
        private readonly List<Competence> _skills = new();
        private readonly List<LearningTool> _selectedTools = new();

        public IReadOnlyList<Competence> Interests => _interests;
        public IReadOnlyList<Competence> Skills => _skills;
        public IReadOnlyList<LearningTool> SelectedTools => _selectedTools;

        public Competence? ChosenCompetence { get; private set; }
        public DateTime? TargetDate { get; private set; }


        public void AddInterest(Competence competence)
        {
            if (!_interests.Contains(competence))
                _interests.Add(competence);
        }

        public void AddSkill(Competence competence)
        {
            if (!_skills.Contains(competence))
                _skills.Add(competence);
        }

        public void SetChosenCompetence(Competence competence)
        {
            ChosenCompetence = competence;
        }

        public void SetLearningTools(List<LearningTool> tools)
        {
            _selectedTools.Clear();
            _selectedTools.AddRange(tools);
        }

        public void SetTargetDate(DateTime date)
        {
            if (date.Date < DateTime.Today)
                throw new ArgumentException("De doel-datum mag niet in het verleden liggen.");
            TargetDate = date.Date;
        }
    }
}