using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Vendra.API.Extentions;
using Vendra.API.Middleware;
using Vendra.Core;
using Vendra.Core.Common;
using Vendra.Core.Data;
using Vendra.Core.Data.Config;
using Vendra.Core.Enities.Identity;
using Vendra.Core.Seeds;
using Vendra.Infrastructure;
namespace Vendra.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.CoreConfiguration(builder.Configuration);
            builder.Services.InfrastructureConfiguration();

            builder.Services.AddIdentityService(builder.Configuration);

            builder.Services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var response = new APIResponse<object>(
                        statusCode: 400,
                        isSuccess: false,
                        errorMessage: "Validation Failed",
                        errors: errors
                    );

                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();

            //Begin calling seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<VendraContext>();
                    await context.Database.MigrateAsync();

                    await ProductBrandSeed.SeedProductBrand(context);
                    await ProductTypeSeed.SeedProductType(context);
                    await ProductSeed.SeedProductAsync(context);
                    await DeliveryMethodSeed.SeedDeliveryMethod(context);

                    var identityContext = services.GetRequiredService<AppIdentityContext>();
                    await identityContext.Database.MigrateAsync();

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await UserSeed.SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred during data seeding.");
                }
            }
            //end calling seed data

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}