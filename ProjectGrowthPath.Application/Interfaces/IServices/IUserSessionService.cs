using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Domain.Entities;


namespace ProjectGrowthPath.Application.Interfaces.IServices;
public interface IUserSessionService
{
    Task<UserProfileDto?> GetAsync();
    Task SetAsync(UserProfile profile);
    void Clear();
    Task RefreshAsync();
}

