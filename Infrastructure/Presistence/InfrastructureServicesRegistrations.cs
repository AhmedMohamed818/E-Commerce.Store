using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using Services.MappingProfile;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public static class InfrastructureServicesRegistrations
    {
       public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
           services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
       services.AddScoped<IDbInitializer, DbInitializer>();
       services.AddScoped<IUnitOfWork, UnitOfWork>();
       services.AddScoped<IBasketRepository, BasketRepository>();
       services.AddScoped<ICacheRepository, CacheRepository>();
            
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
       {
           return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
       });


            // Register your infrastructure services here
            // Example: services.AddScoped<IMyService, MyService>();
            return services;
        }
    }
}
