using ProjectGrowthPath.Application.Interfaces.IServices;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Service;

public class UserSessionService : IUserSessionService
{
    public void Clear()
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile?> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task SetAsync(UserProfile profile)
    {
        throw new NotImplementedException();
    }
}