using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Application.Interfaces;
using YourApp.Application.Exceptions;

namespace ProjectGrowthPath.Application.Service
{
    public class FirstTimeSetupService : IFirstTimeSetupService
    {
        private readonly SetupStateStore _store;
        private readonly IUserProfileService _profileService;
        private readonly IAvatarGenerator _avatarGenerator;
        private readonly ILogger _logger;

        public FirstTimeSetupService(SetupStateStore store, IUserProfileService profileService, IAvatarGenerator avatarGenerator, ILogger logger)
        {
            _store = store;
            _profileService = profileService;
            _avatarGenerator = avatarGenerator;
            _logger = logger;
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


        // Afsluitende methode waar er een profiel wordt aangemaakt
        public async Task FinalizeSetupAsync()
        {
            // Gebruikersprofiel aanmaken
            await ExecuteStepAsync(CreateUserProfile, "Gebruikersprofiel aanmaken");



            // SetupState opschonen
            await ExecuteStepAsync(_store.ClearAsync, "SetupState opschonen");
        }


        public async Task CreateUserProfile()
        {
            var user = _store.CurrentState.NewUser;


            var newUser = new UserProfile
            {
                Name = user.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = await _avatarGenerator.GenerateAvatarAsync(_store.CurrentState.AvatarStyle, _store.CurrentState.SelectedAvatarSeed),
                ApplicationUserId = user.ApplicationUserId
            };

            await _profileService.CreateProfileAsync(newUser);

        }


        private async Task ExecuteStepAsync(Func<Task> step, string stepName)
        {
            try
            {
                _logger.LogInformation("Start stap: {StepName}", stepName);
                await step();
                _logger.LogInformation("Voltooid: {StepName}", stepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij stap: {StepName}", stepName);
                throw new UserFriendlyException($"Er is iets misgegaan bij '{stepName}'. Probeer het later opnieuw.");
            }
        }
    }
}
