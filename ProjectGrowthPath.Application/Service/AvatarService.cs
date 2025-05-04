using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.ValueObjects;

namespace ProjectGrowthPath.Application.Service
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarGenerator _avatarGenerator;
        private readonly IFirstTimeSetupService _setupService;

        public AvatarService(IAvatarGenerator avatarGenerator, IFirstTimeSetupService setupService)
        {
            _avatarGenerator = avatarGenerator;
            _setupService = setupService;
        }

        public async Task<List<(string Seed, string Url)>> LoadOrGenerateAvatarsAsync(string style, SetupState currentState)
        {
            if (currentState.GeneratedAvatars?.Count > 0 == true)
                return currentState.GeneratedAvatars;

            var avatars = await _avatarGenerator.GenerateMultipleAvatarUrlsAsync(style, 6);
            currentState.AvatarStyle = style;
            currentState.GeneratedAvatars = avatars;

            return avatars;
        }

        public async Task<List<(string Seed, string Url)>> ChangeStyleAndGenerateAsync(string newStyle, SetupState currentState)
        {
            currentState.AvatarStyle = newStyle;
            var avatars = await _avatarGenerator.GenerateMultipleAvatarUrlsAsync(newStyle, 6);
            currentState.GeneratedAvatars = avatars;
            return avatars;
        }

        public async Task SelectAvatarAsync(string style, string seed, SetupState currentState)
        {
            await _setupService.SetProfilePictureAsync(style, seed);
            currentState.SelectedAvatarSeed = seed;
        }

        /// <summary>
        /// Combineert stijl wijzigen en nieuwe avatars genereren.
        /// </summary>
        public async Task<List<(string Seed, string Url)>> UpdateStyleAndRegenerateAsync(string style, SetupState state)
        {
            state.AvatarStyle = style;
            state.GeneratedAvatars = await _avatarGenerator.GenerateMultipleAvatarUrlsAsync(style, 6);
            return state.GeneratedAvatars;
        }

        /// <summary>
        /// Initialisatie van de AvatarStep: stijl laden, avatars genereren (indien nodig), geselecteerde avatar ophalen.
        /// </summary>
        public async Task<(List<(string Seed, string Url)> Avatars, string? SelectedSeed)> InitializeAvatarStepAsync(SetupState state)
        {
            var style = state.AvatarStyle ?? "avataaars";
            state.AvatarStyle = style;

            var avatars = await LoadOrGenerateAvatarsAsync(style, state);
            var selectedSeed = string.IsNullOrWhiteSpace(state.SelectedAvatarSeed) ? null : state.SelectedAvatarSeed;

            return (avatars, selectedSeed);
        }
    }
}
