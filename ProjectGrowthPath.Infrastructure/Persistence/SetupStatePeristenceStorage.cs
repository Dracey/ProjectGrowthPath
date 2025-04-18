using Microsoft.JSInterop;
using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
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
            var dto = new SetupStateDto
            {
                NewUser = state.NewUser,
                Interests = state.Interests.ToList(),
                Skills = state.Skills.ToList(),
                SelectedTools = state.SelectedTools.ToList(),
                ChosenCompetence = state.ChosenCompetence,
                TargetDate = state.TargetDate
            };

            var json = JsonSerializer.Serialize(dto);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", StorageKey, json);
        }

        public async Task<SetupState?> LoadAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json)) return null;

            var dto = JsonSerializer.Deserialize<SetupStateDto>(json);
            if (dto == null) return null;

            return new SetupState
            {
                NewUser = dto.NewUser ?? new UserProfile(),
                Interests = dto.Interests,
                Skills = dto.Skills,
                SelectedTools = dto.SelectedTools,
                ChosenCompetence = dto.ChosenCompetence,
                TargetDate = dto.TargetDate
            };
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }
    }
}
