using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.ValueObjects;

namespace ProjectGrowthPath.Application.Service
{
    public class FirstTimeSetupService
    {
        private readonly SetupStateStore _store;
        private readonly IUserProfileService _profileService;


        public FirstTimeSetupService(SetupStateStore store, IUserProfileService repo)
        {
            _store = store;
            _profileService = repo;
        }

        public SetupState CurrentState => _store.CurrentState;

        public void StartSetup(Guid userId) { /* eventueel ophalen of aanmaken */ }

        public void UpdateName(string name) => _store.UpdateName(name);

        public void SelectCompetence(List<Competence> interests) { /* etc. */ }

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
            _store.Clear();
        }
    }

}
