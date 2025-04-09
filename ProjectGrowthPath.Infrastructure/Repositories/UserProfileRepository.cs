using Microsoft.AspNetCore.Identity;
using ProjectGrowthPath.Domain.Entities;
using ProjectGrowthPath.Infrastructure.Identity;
using ProjectGrowthPath.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Application.Interfaces;

namespace ProjectGrowthPath.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfileRepository(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddAsync(UserProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<UserProfile?> GetByIdAsync(Guid id)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(profile => profile.UserID == id);
        }
    }
}
