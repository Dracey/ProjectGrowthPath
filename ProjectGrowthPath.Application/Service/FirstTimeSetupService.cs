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

        public async Task UpdateNameAsync(string name)
        {
            await _store.UpdateNameAsync(name);
        }

        public void SelectInterest(List<Competence> interestsList)
        {
            _store.AddCompetence(interestsList, "Interest");

        }

        public void SelectSkill(List<Competence> skillsList)
        {
            _store.AddCompetence(skillsList, "Skill");

        }

        public void SetProfilePicture(byte[] blob)
        {
            _store.SetProfilePicture(blob);
        }

        public void SetLearningTools(List<LearningTool> tools)
        {
            _store.SetLearningTools(tools);
        }

        public void SetGoalCompetence(Competence competence)
        {
            _store.SetGoalCompetence(competence);
        }

        public void SetTargetDate(DateTime date)
        {
            _store.CurrentState.SetTargetDate(date);
        }

        public void ClearAll()
        {
            _store.CurrentState.ClearAll();
        }

        public async Task FinalizeSetupAsync()
        {
            var newUser = new UserProfile
            {
                Name = _store.CurrentState.NewUser.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = _store.CurrentState.NewUser.ProfilePicture,
            };

            await _profileService.CreateProfileAsync(newUser);
            await _store.ClearAsync();
        }
    }
}