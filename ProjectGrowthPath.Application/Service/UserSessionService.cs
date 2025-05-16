using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Interfaces.IServices;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

public class UserSessionService : IUserSessionService
{
    private UserProfileDto? _cached;
    private readonly IUserProfileService _userProfileService;

    public UserSessionService(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }


    // Haal het profiel op van de gebruiker
    public async Task<UserProfileDto?> GetAsync()
    {
        if (_cached == null)
        {
            var userProfile = await _userProfileService.GetUserProfileByApplicationContext();

            _cached = userProfile != null ? new UserProfileDto()
            {
                UserID = userProfile.UserID,
                ApplicationUserId = userProfile.ApplicationUserId,
                Name = userProfile.Name,
                Level = userProfile.Level,
                Points = userProfile.Points,
                ProfilePicture = userProfile.ProfilePicture
            } : null;
        }

        return _cached;
    }

    // Het profiel setten in de cache
    public Task SetAsync(UserProfile profile)
    {
        _cached = new UserProfileDto()
        {
            UserID = profile.UserID,
            ApplicationUserId = profile.ApplicationUserId,
            Name = profile.Name,
            Level = profile.Level,
            Points = profile.Points,
            ProfilePicture = profile.ProfilePicture
        };

        return Task.CompletedTask;
    }

    // Verwijderen van de cache
    public void Clear() => _cached = null;


    // Refresh 
    public async Task RefreshAsync()
    {
        var userProfile = await _userProfileService.GetUserProfileByApplicationContext();
        if (userProfile != null)
        {
            _cached = new UserProfileDto()
            {
                UserID = userProfile.UserID,
                ApplicationUserId = userProfile.ApplicationUserId,
                Name = userProfile.Name,
                Level = userProfile.Level,
                Points = userProfile.Points,
                ProfilePicture = userProfile.ProfilePicture
            };
        }
        else
        {
            _cached = null;
        }
    }
}