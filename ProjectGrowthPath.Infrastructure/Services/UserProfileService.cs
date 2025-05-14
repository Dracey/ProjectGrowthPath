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
        public async Task CreateProfileAsync(UserProfile newUser , string avatarSeed, string avatarStyle)
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var applicationUserId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            if (user == null) throw new Exception("User niet gevonden");

            var profile = new UserProfile
            {
                UserID = Guid.Parse(applicationUserId),
                ApplicationUserId = applicationUserId,
                Name = newUser.Name,
                Level = 1,
                Points = 0,
                ProfilePicture = newUser.ProfilePicture,
            };

            _dbContext.UserProfiles.Add(profile);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
