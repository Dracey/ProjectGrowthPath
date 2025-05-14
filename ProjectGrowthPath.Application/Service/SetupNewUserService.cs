using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Service.Exceptions;
using ProjectGrowthPath.Application.State;

namespace ProjectGrowthPath.Application.Service;

public class SetupNewUserService
{

    
    private readonly ILogger<SetupNewUserService> _logger;
    private readonly IUserProfileService _userProfileService;
    private readonly SetupStateStore _store;

    SetupNewUserService(ILogger<SetupNewUserService> logger, SetupStateStore store, IUserProfileService userProfileService)
    {
        _store = store;
        _logger = logger;
        _userProfileService = userProfileService;
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

        await ExecuteStepAsync(() => _) 
        
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

