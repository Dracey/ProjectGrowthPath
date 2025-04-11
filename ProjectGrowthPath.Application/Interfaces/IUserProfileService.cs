using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface IUserProfileService
    {

        Task<bool> HasProfileAsync(string userId);
        Task CreateProfileAsync(string userId, string name);
    }
}
