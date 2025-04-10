using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Identity;
using ProjectGrowthPath.Infrastructure.Persistence;

namespace ProjectGrowthPath.Infrastructure.Services
{
    public class UserProfileService(AppDbContext dbContext, UserManager<ApplicationUser> userManager) : IUserProfileService
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        // Check of de gebruiker al een profiel heeft
        public async Task<bool> HasProfileAsync(string userId)
        {
            return await _dbContext.UserProfiles.AnyAsync(up => up.ApplicationUserId == userId);
        }


        // Indien nee, Maak een profiel aan voor de gebruiker
        public async Task CreateProfileAsync(string ÍdentityUserId, string name)
        {
            var user = await _userManager.FindByIdAsync(ÍdentityUserId);

            if (user == null) throw new Exception("User niet gevonden");

            var profile = new UserProfile
            {
                UserID = Guid.Parse(user.Id),
                ApplicationUserId = ÍdentityUserId,
                Name = name,
                Level = 1,
                Points = 0,
                ProfilePicture = null
            };

            _dbContext.UserProfiles.Add(profile);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
