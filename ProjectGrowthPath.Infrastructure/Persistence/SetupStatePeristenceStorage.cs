// SetupStatePersistenceJsInterop.cs
using Microsoft.JSInterop;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Text.Json;

namespace ProjectGrowthPath.Infrastructure.Persistence
{
    public class SetupStatePersistenceJsInterop : ISetupStatePersistence
    {
        private readonly IJSRuntime _jsRuntime;
        private const string StorageKey = "setup-state";

        public SetupStatePersistenceJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveAsync(SetupState state)
        {
            var json = JsonSerializer.Serialize(state);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", StorageKey, json);
        }

        public async Task<SetupState?> LoadAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", StorageKey);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonSerializer.Deserialize<SetupState>(json);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }
    }
}