using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;

public interface IUserProfileService
{ 
    Task<bool> HasProfileAsync(string userId);
    Task<UserProfile> CreateProfileAsync(UserProfile newUser, byte[] avatar);
    Task<UserProfile> GetUserProfileByApplicationContext();
}

