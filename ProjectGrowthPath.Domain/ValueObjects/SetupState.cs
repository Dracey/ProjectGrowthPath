using ProjectGrowthPath.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectGrowthPath.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjectGrowthPath.Domain.ValueObjects
{
    public class SetupState
    {
        public UserProfile NewUser { get; init; }
        private List<Competence> _interests = new();
        private List<Competence> _skills = new();
        private List<LearningTool> _selectedTools = new();
        public Competence? ChosenCompetence { get; private set; }
        public DateTime? TargetDate { get; private set; }

        public IReadOnlyList<Competence> Interests => _interests.AsReadOnly();
        public IReadOnlyList<Competence> Skills => _skills.AsReadOnly();
        public IReadOnlyList<LearningTool> SelectedTools => _selectedTools.AsReadOnly();

        public SetupState()
        {
            NewUser = new UserProfile();
        }

        public void AddInterest(Competence interest)
        {
            _interests.Add(interest);
        }

        public void AddSkill(Competence skill)
        {
            _skills.Add(skill);
        }

        public void SetChosenCompetence(Competence competence)
        {
            ChosenCompetence = competence;
        }

        public void SetTargetDate(DateTime date)
        {
            TargetDate = date;
        }

        public void SetLearningTools(List<LearningTool> tools)
        {
            _selectedTools = tools;
        }

        public void ClearAll()
        {
            _interests.Clear();
            _skills.Clear();
            _selectedTools.Clear();
            ChosenCompetence = null;
            TargetDate = null;
        }
    }
}
