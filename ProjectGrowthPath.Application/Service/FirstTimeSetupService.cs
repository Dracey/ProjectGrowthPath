using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.State;

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

        public void StartSetup(Guid userId) { /* eventueel ophalen of aanmaken */ }

        public void UpdateName(string name) => _store.UpdateName(name);

        public void SelectCompetence(List<Competence> interests) { /* etc. */ }

        public void FinalizeSetup()
        {
            var newUser = new UserProfile()
            {
                Name = _store.CurrentState.NewUser.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = _store.CurrentState.NewUser.ProfilePicture,
            };

            _profileService.CreateProfileAsync(newUser);
            _store.Clear();
        }
    }

}
