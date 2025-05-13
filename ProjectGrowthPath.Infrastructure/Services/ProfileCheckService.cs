using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectGrowthPath.Application.Interfaces;
namespace ProjectGrowthPath.Infrastructure.Services
{
    public class ProfileCheckService(IUserProfileService profileService, NavigationManager navigation, AuthenticationStateProvider authStateProvider) : IProfileCheckService
    {
        private readonly IUserProfileService _profileService = profileService;
        private readonly NavigationManager _navigation = navigation;
        private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

        public async Task CheckAndRedirectIfNeeded()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    // Controleer of de gebruiker al een profiel heeft
                    var hasProfile = await _profileService.HasProfileAsync(userId);

                    if (!hasProfile)
                    {
                        // Als het profiel niet bestaat, stuur de gebruiker naar de profielinstelling
                        _navigation.NavigateTo("/profile/setup");
                    }
                }
            }
        }
    }
}