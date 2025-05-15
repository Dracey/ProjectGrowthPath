using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGrowthPath.Domain.Entities;

namespace ProjectGrowthPath.Application.Interfaces;

public interface IUserProfileService
{ 
    Task<bool> HasProfileAsync(string userId);
    Task<UserProfile> CreateProfileAsync(UserProfile newUser, string avatarSeed, string avatarStyle);
    Task<UserProfile> GetUserProfileByApplicationIDAsync(string applicationuserID);
}

