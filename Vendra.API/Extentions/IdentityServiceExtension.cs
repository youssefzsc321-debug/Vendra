using Microsoft.AspNetCore.Identity;
using Vendra.Core.Enities.Identity;

namespace Vendra.API.Extentions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
