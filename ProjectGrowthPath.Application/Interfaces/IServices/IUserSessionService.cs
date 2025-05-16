using ProjectGrowthPath.Domain.Entities;


namespace ProjectGrowthPath.Application.Interfaces.IServices;
public interface IUserSessionService
{
    Task<UserProfile?> GetAsync();
    Task SetAsync(UserProfile profile);
    void Clear();
}

