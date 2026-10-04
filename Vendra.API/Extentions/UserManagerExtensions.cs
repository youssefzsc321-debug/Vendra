using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vendra.Core.Enities.Identity;

namespace Vendra.API.Extentions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserWithAddressByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(u=>u.Email==email);

            return user;

        }

    }
}
