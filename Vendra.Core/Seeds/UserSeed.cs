using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Identity;
namespace Vendra.Core.Seeds
{
    public static class UserSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user=new AppUser() 
                {
                    FullName = "Youssef Abuzaid",
                    UserName = "youssef",
                    Email = "youssef@vendra.com",
                    Address = new Address()
                    {
                        FirstName = "Youssef",
                        LastName = "Abuzaid",
                        Country = "Egypt",
                        City = "Cairo",
                        Street = "123 Main St"
                    }
                };
                await userManager.CreateAsync(user,"753951420Tt#");
            }
        }
    }
}
