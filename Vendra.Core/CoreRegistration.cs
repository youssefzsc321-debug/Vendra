using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Enities.Identity;
using Vendra.Core.Interfaces;


namespace Vendra.Core
{
    public static class CoreRegistration
    {
        public static IServiceCollection CoreConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<VendraContext>(options =>
                options.UseSqlServer(connectionString));

            var connectionStringIdentity = configuration.GetConnectionString("IdentityConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer(connectionStringIdentity));

            

            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            services.AddTransient<Helpers.ProductUrlResolver>(); 

            services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                var connection = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(connection);
            });

            return services;


        }
    }
}
