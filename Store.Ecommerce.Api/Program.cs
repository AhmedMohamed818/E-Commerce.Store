
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Presistence;
using Presistence.Data;
using Services;
using Services.Abstractions;
using Services.MappingProfile;
using Shared.ErrorsModels;
using Store.Ecommerce.Api.Extensions;
using Store.Ecommerce.Api.Middlewares;

namespace Store.Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegistarAllServices(builder.Configuration);


            var app = builder.Build();
            await app.ConfiguerMiddleWares();

            app.Run();
        }
    }
}
