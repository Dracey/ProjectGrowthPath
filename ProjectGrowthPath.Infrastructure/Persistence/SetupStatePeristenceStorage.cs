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

        // Opslaan van session gegevens
        public async Task SaveAsync(SetupState state)
        {
            var dto = new SetupStateDto
            {
                NewUser = state.NewUser,
                AvatarStyle = state.AvatarStyle,
                SelectedAvatarSeed = state.SelectedAvatarSeed,
                GeneratedAvatars = state.GeneratedAvatars
                    .Select(a => new AvatarInfoDto { Seed = a.Seed, Url = a.Url })
                    .ToList(),
                Interests = state.Interests.ToList(),
                Skills = state.Skills.ToList(),
                SelectedTools = state.SelectedTools.ToList(),
                ChosenCompetence = state.ChosenCompetence,
                TargetDate = state.TargetDate
            };

            var json = JsonSerializer.Serialize(dto);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", StorageKey, json);
        }


        // Laden van session gegevens. 
        public async Task<SetupState?> LoadAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json)) return new SetupState();  

            var dto = JsonSerializer.Deserialize<SetupStateDto>(json);
            if (dto == null) return new SetupState();  

            var state = new SetupState();

            
            state.NewUser.SetName(dto.NewUser?.Name ?? string.Empty);  

            if (dto.TargetDate.HasValue)
            {
                state.SetTargetDate(dto.TargetDate.Value);
            }

            if (dto.AvatarStyle != null)
            {
                state.AvatarStyle = dto.AvatarStyle;
            }

            if (!string.IsNullOrWhiteSpace(dto.SelectedAvatarSeed))
            {
                state.SelectedAvatarSeed = dto.SelectedAvatarSeed;
            }

            if (dto.GeneratedAvatars != null)
            {
                state.GeneratedAvatars = dto.GeneratedAvatars
                    .Select(a => (a.Seed, a.Url))
                    .ToList();
            }

            if (dto.Interests != null)
            {
                foreach (var interest in dto.Interests)
                {
                    state.AddInterest(interest);
                }
            }
            if (dto.Skills != null)
            {
                foreach (var skill in dto.Skills)
                {
                    state.AddSkill(skill);
                }
            }
            if (dto.SelectedTools != null)
            {
                state.SetLearningTools(dto.SelectedTools);
            }
            if (dto.ChosenCompetence != null)
            {
                state.SetChosenCompetence(dto.ChosenCompetence);
            }
            return state;
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }
    }
}
