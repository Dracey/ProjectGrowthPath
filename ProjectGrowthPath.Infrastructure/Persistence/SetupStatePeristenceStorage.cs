﻿using Microsoft.JSInterop;
using ProjectGrowthPath.Application.DTOs;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Domain.ValueObjects;
using System.Text.Json;
using System.Xml;
using ProjectGrowthPath.Application.Interfaces;

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
                SelectedInterests = state.SelectedInterests.ToDictionary(entry => entry.Key, entry => entry.Value),
                SelectedSkills = state.SelectedSkills.ToDictionary(entry => entry.Key, entry => entry.Value),
                ChosenCompetence = state.ChosenCompetence,
                SelectedTools = state.SelectedTools.ToList(),
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

            var state = new SetupState
            {
                NewUser = { Name = dto.NewUser?.Name ?? string.Empty },
                AvatarStyle = dto.AvatarStyle,
                SelectedAvatarSeed = dto.SelectedAvatarSeed ?? string.Empty,
                SelectedInterests = dto.SelectedInterests ?? new Dictionary<int, Competence>(),
                SelectedSkills = dto.SelectedSkills ?? new Dictionary<int, Competence>(),
                GeneratedAvatars = dto.GeneratedAvatars?.Select(a => (a.Seed, a.Url)).ToList() ?? new List<(string, string)>(),
                SelectedTools = dto.SelectedTools ?? new List<int>()
            };

            if (dto.TargetDate.HasValue) state.SetTargetDate(dto.TargetDate.Value);
            if (dto.ChosenCompetence != null) state.SetChosenCompetence(dto.ChosenCompetence);
         
            return state;
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);
        }
    }
}
