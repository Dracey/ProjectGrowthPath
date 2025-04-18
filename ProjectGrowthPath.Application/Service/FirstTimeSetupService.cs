using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service
{
    public class FirstTimeSetupService
    {
        private readonly SetupStateStore _store;
        private readonly IUserProfileService _profileService;

        public FirstTimeSetupService(SetupStateStore store, IUserProfileService profileService)
        {
            _store = store;
            _profileService = profileService;
        }


        // Wizard update methodes
        public async Task UpdateNameAsync(string name)
        {
            await _store.UpdateNameAsync(name);
        }

        public async Task SelectInterestsAsync(List<Competence> interests)
        {
            foreach (var interest in interests)
                await _store.AddInterestAsync(interest);
        }

        public async Task SelectSkillsAsync(List<Competence> skills)
        {
            foreach (var skill in skills)
                await _store.AddSkillAsync(skill);
        }

        public async Task SetProfilePictureAsync(byte[] picture)
        {
            await _store.SetProfilePictureAsync(picture);
        }

        public async Task SetLearningToolsAsync(List<LearningTool> tools)
        {
            await _store.SetLearningToolsAsync(tools);
        }

        public async Task SetGoalCompetenceAsync(Competence competence)
        {
            await _store.SetChosenCompetenceAsync(competence);
        }

        public async Task SetTargetDateAsync(DateTime targetDate)
        {
            await _store.SetTargetDateAsync(targetDate);
        }

        public async Task ClearWizardAsync()
        {
            await _store.ClearAsync();
        }


        // Eindmethode die alles bij elkaar brengt
        public async Task FinalizeSetupAsync()
        {
            var user = _store.CurrentState.NewUser;

            var newUser = new UserProfile
            {
                Name = user.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = user.ProfilePicture,
                ApplicationUserId = user.ApplicationUserId
            };

            await _profileService.CreateProfileAsync(newUser);
            await _store.ClearAsync();
        }
    }
}
