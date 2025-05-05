using Microsoft.Extensions.Logging;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.ValueObjects;

namespace ProjectGrowthPath.Application.State
{
    public class SetupStateStore
    {
        private readonly ISetupStatePersistence _persistence;
        private readonly ILogger<SetupStateStore> _logger;
        public SetupState CurrentState { get; private set; }


        public SetupStateStore(ISetupStatePersistence persistence, ILogger<SetupStateStore> logger)
        {
            _persistence = persistence;
            CurrentState = new SetupState();
            _logger = logger;

        }

        // Persistence methodes
        public async Task LoadAsync()
        {
            var loaded = await _persistence.LoadAsync();
            CurrentState = loaded ?? new SetupState();
        }

        public async Task SaveAsync()
            => await _persistence.SaveAsync(CurrentState);

        public async Task ClearAsync()
        {
            await _persistence.ClearAsync();
            CurrentState = new SetupState();
        }

        public async Task UpdateStateAsync(Action<SetupState> update, string? reason = null)
        {
            update(CurrentState);
            _logger.LogInformation("SetupState updated{Reason}", reason != null ? $" - {reason}" : "");
            await SaveAsync();
        }
    }
}