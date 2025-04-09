using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserIdAsync(ClaimsPrincipal user);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<(bool success, string[] errors)> CreateUserAsync(string email, string password);
        Task SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}
