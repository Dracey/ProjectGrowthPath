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
    private readonly SetupStateStore _store;

    SetupNewUserService(ILogger<SetupNewUserService> logger, SetupStateStore store, IUserProfileService userProfileService, IUserCompetenceRepository userCompetenceRepository)
    {
        _store = store;
        _logger = logger;
        _userProfileService = userProfileService;
        _userCompetenceRepository = userCompetenceRepository;
    }

    public async Task FinishUpSetupAsync()
    {
        if (_store.CurrentState == null)
        {
            throw new UserFriendlyException("Er is geen informatie opgeslagen.");
        }


        // Eerste stap, gebruiker aanmaken.
        await ExecuteStepAsync(() =>
                _userProfileService.CreateProfileAsync(_store.CurrentState.NewUser, _store.CurrentState.SelectedAvatarSeed, _store.CurrentState.AvatarStyle), "Gebruikersprofiel aanmaken");

        // Nieuwe gebruiker ophalen van de database
        var savedUser = await _userProfileService.GetUserProfileByApplicationIDAsync(_store.CurrentState.NewUser.ApplicationUserId);

        // Voeg interesses toe aan gebruiker
        await ExecuteStepAsync(() => _userCompetenceRepository.AddUserCompetenceAsync(_store.CurrentState.SelectedInterests, savedUser.UserID, 0), "Interesses toevoegen aan gebruiker");

        // Voeg skills toe aan gebruiker
        await ExecuteStepAsync(() => _userCompetenceRepository.AddUserCompetenceAsync(_store.CurrentState.SelectedSkills, savedUser.UserID, 1), "Skills toevoegen aan gebruiker");

      


        await ExecuteStepAsync(() => _store.ClearAsync(), "SetupState opschonen");
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

