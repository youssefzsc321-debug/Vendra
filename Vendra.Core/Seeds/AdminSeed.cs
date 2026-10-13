using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vendra.Core.Enities.Identity;

namespace Vendra.Core.Seeds
{
    public static class AdminSeed
    {
        public static async Task SeedAdminsAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            var adminEmail = "youssef@vendra.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new AppUser()
                {
                    FullName = "Youssef Abuzaid",
                    UserName = "youssef",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "753951420Tt#");
                if (!result.Succeeded) return;
            }

            var isInAdminRole = await userManager.IsInRoleAsync(adminUser, "Admin");
            if (!isInAdminRole)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}