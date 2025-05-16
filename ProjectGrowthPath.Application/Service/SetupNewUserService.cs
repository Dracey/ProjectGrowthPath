using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Interfaces.IRepository;
using ProjectGrowthPath.Application.Service.Exceptions;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

public class SetupNewUserService
{
    private readonly ILogger<SetupNewUserService> _logger;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserCompetenceRepository _userCompetenceRepository;
    private readonly IGoalRepository _goalRepository;
    private readonly IGoalLearningToolRepository _goalLearningToolRepository;
    private readonly IAvatarGenerator _avatarGenerator;
    private readonly SetupStateStore _store;

    public SetupNewUserService(ILogger<SetupNewUserService> logger, SetupStateStore store, IUserProfileService userProfileService, IUserCompetenceRepository userCompetenceRepository, IGoalRepository goalRepository, IGoalLearningToolRepository goalLearningToolRepository, IAvatarGenerator avatarGenerator)
    {
        _store = store;
        _logger = logger;
        _userProfileService = userProfileService;
        _userCompetenceRepository = userCompetenceRepository;
        _goalRepository = goalRepository;
        _goalLearningToolRepository = goalLearningToolRepository;
        _avatarGenerator = avatarGenerator;
    }


    // Afsluiten setup en opslaan in de database
    public async Task FinishUpSetupAsync()
    {
        if (_store.CurrentState == null)
        {
            throw new UserFriendlyException("Er is geen informatie opgeslagen.");
        }


        // Avatar to bytes 
        byte[] avatar =
           await _avatarGenerator.GenerateAvatarAsync(_store.CurrentState.AvatarStyle,
                _store.CurrentState.SelectedAvatarSeed);

        // Eerste stap, gebruiker aanmaken.
         UserProfile savedUser = await ExecuteStepAsync(() =>
                _userProfileService.CreateProfileAsync(_store.CurrentState.NewUser, avatar), "Gebruikersprofiel aanmaken");

        // Voeg interesses toe aan gebruiker
        _ = await ExecuteStepAsync(() => _userCompetenceRepository.AddUserCompetenceAsync(_store.CurrentState.SelectedInterests, savedUser.UserID, 0), "Interesses toevoegen aan gebruiker");

        // Voeg skills toe aan gebruiker
        _ = await ExecuteStepAsync(() => _userCompetenceRepository.AddUserCompetenceAsync(_store.CurrentState.SelectedSkills, savedUser.UserID, 1), "Skills toevoegen aan gebruiker");

        Goal newGoal = CreateGoal(savedUser.UserID);

        // Voeg doel toe aan gebruiker
        Goal addedGoal = await ExecuteStepAsync(() => _goalRepository.Add(newGoal), "Goal toevoegen");


        // Voeg leermiddelen toe aan doel
        _ = await ExecuteStepAsync(() => _goalLearningToolRepository.Add(_store.CurrentState.SelectedTools, addedGoal.GoalID), "Leermiddelen toevoegen aan doel");

        await _store.ClearAsync();
    }


    // Voer een stap uit en log de voortgang
    private async Task<T> ExecuteStepAsync<T>(Func<Task<T>> step, string stepName)
    {
        try
        {
            _logger.LogInformation("Start stap: {StepName}", stepName);
            var result = await step();
            _logger.LogInformation("Voltooid: {StepName}", stepName);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fout bij stap: {StepName}", stepName);
            throw new UserFriendlyException($"Er is iets misgegaan bij '{stepName}'. Probeer het later opnieuw.");
        }
    }


    // Maak een doel aan voor de gebruiker
    private Goal CreateGoal(Guid userId)
    {
        if (_store.CurrentState == null)
        {
            throw new UserFriendlyException("Er is geen informatie opgeslagen.");
        }

        return new Goal
        {
            UserID = userId,
            Description = _store.CurrentState.ChosenCompetence.Name,
            Amount = _store.CurrentState.SelectedTools.Count,
            IsCompleted = false,
            StartDate = DateTime.UtcNow.Date,
            EndDate = _store.CurrentState.TargetDate.Value.ToUniversalTime(),
        };
    }
}

