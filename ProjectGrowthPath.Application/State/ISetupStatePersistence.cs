using ProjectGrowthPath.Domain.ValueObjects;


namespace ProjectGrowthPath.Application.State
{
    public interface ISetupStatePersistence
    {
        Task<SetupState?> LoadAsync();
        Task SaveAsync(SetupState state);
        Task ClearAsync();
    }
}
