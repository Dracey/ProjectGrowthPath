using ProjectGrowthPath.Domain.ValueObjects;
namespace ProjectGrowthPath.Application.Interfaces;
public interface IAvatarService
{
    Task<List<(string Seed, string Url)>> LoadOrGenerateAvatarsAsync(string style, SetupState currentState);
    Task<List<(string Seed, string Url)>> ChangeStyleAndGenerateAsync(string newStyle, SetupState currentState);
    Task SelectAvatarAsync(string style, string seed, SetupState currentState);
    Task<List<(string Seed, string Url)>> UpdateStyleAndRegenerateAsync(string style, SetupState state);
    Task<(List<(string Seed, string Url)> Avatars, string? SelectedSeed)> InitializeAvatarStepAsync(SetupState state);
}