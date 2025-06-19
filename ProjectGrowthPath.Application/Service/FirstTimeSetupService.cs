using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Application.Interfaces;

namespace ProjectGrowthPath.Application.Service
{
    public class FirstTimeSetupService : IFirstTimeSetupService
    {
        private readonly SetupStateStore _store;
        private readonly IAvatarGenerator _avatarGenerator;
        private readonly SetupNewUserService _setupNewUserService;

        public FirstTimeSetupService(SetupStateStore store, IUserProfileService profileService, IAvatarGenerator avatarGenerator, SetupNewUserService setupNewUserService)
        {
            _store = store;
            _avatarGenerator = avatarGenerator;
            _setupNewUserService = setupNewUserService;
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


        public async Task ToggleLearningToolAsync(int toolId)
        {
            var isAlreadySelected = _store.CurrentState.SelectedTools.Contains(toolId);

            await _store.UpdateStateAsync(
                s => s.ToggleLearningTool(toolId),
                $"Leermiddel {(isAlreadySelected ? "verwijderd" : "toegevoegd")}: ID {toolId}"
            );
        }


        // Afsluitende methode (aparte service) aanroepen waar er een profiel wordt aangemaakt
        public async Task FinalizeSetupAsync()
        {
            await _setupNewUserService.FinishUpSetupAsync();
        }
    }
}
