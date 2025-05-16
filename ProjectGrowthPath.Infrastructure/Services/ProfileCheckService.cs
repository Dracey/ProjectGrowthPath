using Microsoft.AspNetCore.Components;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Interfaces.IServices;

namespace ProjectGrowthPath.Infrastructure.Services;

public class ProfileCheckService : IProfileCheckService
{
    private readonly IUserSessionService _userSessionService;
    private readonly NavigationManager _navigation;

    public ProfileCheckService(NavigationManager navigation, IUserSessionService userSessionService)
    {
        _navigation = navigation;
        _userSessionService = userSessionService;
    }

    public async Task CheckAndRedirectIfNeeded()
    {
        var user = await _userSessionService.GetAsync();

        if (user == null)
        {
            _navigation.NavigateTo("/profile/setup");
        }
    }
}