using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;

namespace ProjectGrowthPath.Application.State
{
    public class SetupStateStore
    {
        private readonly ISetupStatePersistence _persistence;
        public SetupState CurrentState { get; private set; }


        public SetupStateStore(ISetupStatePersistence persistence)
        {
            _persistence = persistence;
            CurrentState = new SetupState();
        }

        // Persistence methodes
        public async Task LoadAsync()
        {
            var loaded = await _persistence.LoadAsync();
            CurrentState = loaded ?? new SetupState();
        }

        public async Task SaveAsync()
            => await _persistence.SaveAsync(CurrentState);

        public async Task ClearAsync()
        {
            await _persistence.ClearAsync();
            CurrentState = new SetupState();
        }


        // Wizard update methodes
        public async Task UpdateNameAsync(string name)
        {
            CurrentState.NewUser.Name = name;
            await SaveAsync();
        }

        public async Task SetProfilePictureAsync(byte[] blob)
        {
            CurrentState.NewUser.ProfilePicture = blob;
            await SaveAsync();
        }

        public async Task AddInterestAsync(Competence competence)
        {
            CurrentState.Interests.Add(competence);
            await SaveAsync();
        }

        public async Task AddSkillAsync(Competence competence)
        {
            CurrentState.Skills.Add(competence);
            await SaveAsync();
        }

        public async Task SetChosenCompetenceAsync(Competence competence)
        {
            CurrentState.ChosenCompetence = competence;
            await SaveAsync();
        }

        public async Task SetLearningToolsAsync(List<LearningTool> tools)
        {
            CurrentState.SelectedTools = tools;
            await SaveAsync();
        }

        public async Task SetTargetDateAsync(DateTime date)
        {
            CurrentState.TargetDate = date;
            await SaveAsync();
        }
    }
}