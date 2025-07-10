﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.MappingProfile;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            // Register your application services here
            // Example: services.AddScoped<IMyService, MyService>();
           // services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
