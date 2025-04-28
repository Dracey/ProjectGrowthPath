using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Xml.Linq;

namespace ProjectGrowthPath.Application.Service
{
    public class FirstTimeSetupService
    {
        private readonly SetupStateStore _store;
        private readonly IUserProfileService _profileService;
        private readonly IAvatarGenerator _avatarGenerator;

        public FirstTimeSetupService(SetupStateStore store, IUserProfileService profileService, IAvatarGenerator avatarGenerator)
        {
            _store = store;
            _profileService = profileService;
            _avatarGenerator = avatarGenerator;
        }


        // Wizard update methodes
        public async Task UpdateNameAsync(string name)
        {
            await _store.UpdateStateAsync(s => s.NewUser.SetName(name), "Naam ingesteld");
        }

        public async Task SetProfilePictureAsync(string style, string seed)
        {
            var avatarBytes = await _avatarGenerator.GenerateAvatarAsync(style, seed);

            await _store.UpdateStateAsync(s =>
                {
                    s.NewUser.SetProfilePicture(avatarBytes);
                    s.SelectedAvatarSeed = seed;
                }, $"Avatar gekozen via DiceBear (style: {style}, seed: {seed})");
        }


        public async Task UpdateCompetenceDictionary(int id, Competence? competence, string type)
        {
            string label = type switch
            {
                "interests" => "Interesse",
                "skills" => "Vaardigheid",
                _ => throw new ArgumentException("Invalid type specified. Use 'interests' or 'skills'.")
            };

            string message = $"{label} {(competence?.Name ?? "verwijderd")}";

            await _store.UpdateStateAsync(s =>
            {
                var targetDict = type == "interests" ? s.SelectedInterests : s.SelectedSkills;

                if (competence == null)
                    targetDict.Remove(id);
                else
                    targetDict[id] = competence;

            }, message);
        }


        public async Task SetGoalCompetenceAsync(Competence competence)
        {
            await _store.UpdateStateAsync(s => s.SetChosenCompetence(competence), $"Gekozen doel competentie: {competence.Name} vastgelegd.");
        }

        
        public async Task SetTargetDateAsync(DateTime date)
        {
            await _store.UpdateStateAsync(s => s.SetTargetDate(date), $"Doel datum ingesteld: {date.ToShortDateString()}");
        }

        //public async Task SetLearningToolsAsync(List<LearningTool> tools)
        //{
        //    await _store.SetLearningToolsAsync(tools);
        //}

        
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
