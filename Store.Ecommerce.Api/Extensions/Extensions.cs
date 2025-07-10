using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Presistence;
using Presistence.Data;
using Presistence.Identity;
using Services;
using Shared;
using Shared.ErrorsModels;
using Store.Ecommerce.Api.Middlewares;
using System.Text;
using System.Threading.Tasks;

namespace Store.Ecommerce.Api.Extensions
{
    public static class Extensions
    {
     public static IServiceCollection RegistarAllServices (this IServiceCollection services , IConfiguration configuration)
        {
            // Add services to the container.

            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddInfrastructureServices(configuration);
            services.AddIdentityServices();
            services.AddApplicationServices(configuration);
            services.ConfigureJwtService(configuration);


            services.AddCors(config =>
            {
                config.AddPolicy("CorsPolicy", option =>
                {
                    option.AllowAnyOrigin()
                           .AllowAnyMethod()
                           //.AllowAnyHeader();
                           .WithOrigins("http://localhost:4200/"); // Specify allowed origins if needed
                });
            });

            services.Configure<ApiBehaviorOptions>(config =>
            {
                // Disable the default model state validation behavior
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                         .Select(m => new ValidationError()
                         {
                             Field = m.Key,
                             Errors = m.Value.Errors.Select(err => err.ErrorMessage)
                         });
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors,
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
          }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Add built-in services here if needed
            return services;
        }
        private static IServiceCollection ConfigureJwtService(this IServiceCollection services , IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
           services.AddIdentity<AppUser , IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }


        public static async Task<WebApplication> ConfiguerMiddleWares (this WebApplication app)
        {
            await app.InitializeDataBaseAsync();

            app.GlobalErrorHandling();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");



            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            // Add any additional configurations for the WebApplication here if needed
            return app;
        }
        private static async Task< WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            //app.Services.CreateScope().ServiceProvider.GetRequiredService<IDbInitializer>().InitializeAsync().Wait();
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await services.InitializeAsync();
            await services.InitializeIdentityAsync();

            return app;
        }
        private static WebApplication GlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;

        }





































    }
}
