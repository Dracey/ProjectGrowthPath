using Microsoft.AspNetCore.Http;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Text.Json;

namespace ProjectGrowthPath.Infrastructure.Persistence
{
    public class SetupStatePersistence : ISetupStatePersistence
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string Key = "setup_state";

        public SetupStatePersistence(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SetupState?> LoadAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return null;

            var json = session.GetString(Key);
            if (string.IsNullOrEmpty(json)) return null;

            return await Task.FromResult(JsonSerializer.Deserialize<SetupState>(json));
        }

        public async Task SaveAsync(SetupState state)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return;

            var json = JsonSerializer.Serialize(state);
            session.SetString(Key, json);
            await Task.CompletedTask;
        }

        public async Task ClearAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Remove(Key);
            await Task.CompletedTask;
        }
    }
}