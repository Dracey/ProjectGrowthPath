using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Identity;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authProvider;

        public UserProfileService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, AuthenticationStateProvider authProvider)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _authProvider = authProvider;
        }

        // Check of de gebruiker al een profiel heeft
        public async Task<bool> HasProfileAsync(string userId)
        {
            return await _dbContext.UserProfiles.AnyAsync(up => up.ApplicationUserId == userId);
        }

        // Maak een nieuw profiel aan
        public async Task<UserProfile> CreateProfileAsync(UserProfile newUser , byte[] avatar)
        {
            var (_, applicationUserId) = await GetAuthenticatedUserAsync();

            var profile = new UserProfile
            {
                UserID = Guid.Parse(applicationUserId),
                ApplicationUserId = applicationUserId,
                Name = newUser.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = avatar,
            };

            var entityEntry = await _dbContext.UserProfiles.AddAsync(profile);

            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        // Haal het profiel op van de gebruiker
        public async Task<UserProfile> GetUserProfileByApplicationContext()
        {
            var (_, applicationUserId) = await GetAuthenticatedUserAsync();

            var profile = await _dbContext.UserProfiles
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);

            if (profile == null)
                throw new Exception("Profiel niet gevonden");

            return profile;
        }

        // Haal het profiel op van de gebruiker met een specifieke ID uit ASP.NET Identity
        private async Task<(ClaimsPrincipal user, string applicationUserId)> GetAuthenticatedUserAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user == null || !user.Identity.IsAuthenticated)
                throw new Exception("User niet gevonden of niet ingelogd.");

            var applicationUserId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(applicationUserId))
                throw new Exception("ApplicationUserId ontbreekt.");

            return (user, applicationUserId);
        }

    }
}
