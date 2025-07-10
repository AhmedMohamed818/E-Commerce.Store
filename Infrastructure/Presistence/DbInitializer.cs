using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreDbContext _storeDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreDbContext context, StoreDbContext storeDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _storeDbContext = storeDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeAsync()
        {

            if (_context.Database.GetPendingMigrations().Any())
            {
                // Apply migrations if there are any pending migrations
                await _context.Database.MigrateAsync();
                // Database already exists

            }
            if (!_context.ProductTypes.Any())
            {
                // Seed the database with initial data
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Any())
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();

                }

            }
            if (!_context.ProductBrands.Any())
            {
                // Seed the database with initial data
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands is not null && brands.Any())
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }
            if (!_context.Products.Any())
            {
                // Seed the database with initial data
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
            // Add more seeding logic for other entities if needed
            if (!_context.DeliveryMethods.Any())
            {
                // Seed the database with initial data
                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliveryMethods is not null && deliveryMethods.Any())
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await _context.SaveChangesAsync();
                }
            }
            
            // Save changes to the database
        }

        public async Task InitializeIdentityAsync()
        {
            if (_storeDbContext.Database.GetPendingMigrations().Any())
            {
                // Apply migrations if there are any pending migrations
                await _storeDbContext.Database.MigrateAsync();
                // Database already exists

            }
            // Check if the roles exist, if not create them
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",

                });
                //var roles = new List<IdentityRole>
                //{
                //    new IdentityRole { Name = "Admin" },
                //    new IdentityRole { Name = "User" }
                //};
                //foreach (var role in roles)
                //{
                //    await _roleManager.CreateAsync(role);
                //}
            }
            // Check if there are any users in the database
            if (!_userManager.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "1234567890"
                };
                var adminUser = new AppUser()
                {
                    DisplayName = " Admin",
                    Email = "adminUser@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "1234567890"
                };
                await _userManager.CreateAsync(superAdmin, "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");
                // Assign roles to the users
                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");



            }

            }



























    }
}


