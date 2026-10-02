using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Repositories;

namespace Vendra.Infrastructure
{
    public static class infrastrucureRegistration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services)
        {
            

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IUnitOfWorkRepo, UnitOfWorkRepo>();
            services.AddScoped<IBasketRepo, BasketRepo>();
            
            return services;


        }
    }
}
