using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;

namespace ProjectGrowthPath.Application.State
{
    public class SetupStateStore
    {
        private readonly ISetupStatePersistence _persistence;
        public SetupState CurrentState { get; private set; } = new SetupState();

        public SetupStateStore(ISetupStatePersistence persistence)
        {
            _persistence = persistence;
        }

        public async Task LoadAsync()
        {
            var loaded = await _persistence.LoadAsync();
            CurrentState = loaded ?? new SetupState();
        }

        public async Task SaveAsync()
        {
            await _persistence.SaveAsync(CurrentState);
        }

        public async Task UpdateNameAsync(string name)
        {
            CurrentState.NewUser.Name = name;
            await SaveAsync();
        }

        public async Task ClearAsync()
        {
            await _persistence.ClearAsync();
            CurrentState = new SetupState();
        }

        public void UpdateName(string name)
        {
            CurrentState.NewUser.Name = name;
        }

        public void AddCompetence(List<Competence> competences, string type)
        {
            if (type == "Interest")
                CurrentState.AddInterest(competences.First());
            else if (type == "Skill")
                CurrentState.AddSkill(competences.First());
        }

        public void SetProfilePicture(byte[] blob)
        {
            CurrentState.NewUser.ProfilePicture = blob;
        }

        public void SetLearningTools(List<LearningTool> tools)
        {
            CurrentState.SetLearningTools(tools);
        }

        public void SetGoalCompetence(Competence competence)
        {
            CurrentState.SetChosenCompetence(competence);
        }

        public void SetTargetDate(DateTime date)
        {
            CurrentState.SetTargetDate(date);
        }
    }
}