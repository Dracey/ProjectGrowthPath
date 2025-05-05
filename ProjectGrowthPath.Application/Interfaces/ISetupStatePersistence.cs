using ProjectGrowthPath.Domain.ValueObjects;


namespace ProjectGrowthPath.Application.Interfaces
{
    public interface ISetupStatePersistence
    {
        Task SaveAsync(SetupState state);
        Task<SetupState?> LoadAsync();
        Task ClearAsync();
    }
}
