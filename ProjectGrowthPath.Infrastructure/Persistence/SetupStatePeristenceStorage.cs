using Microsoft.JSInterop;
using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Text.Json;
using System.Xml;

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
            
            var state = new SetupState();

            state.NewUser.SetName(dto.NewUser.Name);
            state.SetChosenCompetence(dto.ChosenCompetence);
            state.SetTargetDate(dto.TargetDate.Value);
            foreach(var interest in dto.Interests) state.AddInterest(interest);
            foreach (var skill in dto.Skills) state.AddSkill(skill);
            state.SetLearningTools(dto.SelectedTools);

            return state;
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }
    }
}
