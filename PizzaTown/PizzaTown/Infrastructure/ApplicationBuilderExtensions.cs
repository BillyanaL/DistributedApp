using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;

namespace PizzaTown.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();

            MigrateDatabase(dbContext);
            SeedRoles(dbContext);
            SeedAdmin(dbContext);
            SeedCategories(dbContext);

            return app;
        }

        private static void MigrateDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            if (dbContext.Roles.Any())
            {
                return;
            }

            dbContext.Roles.AddRange(new[]
            {
                new Role {Id = "77fc6a94-02d7-4993-99e4-ca0bf19c2c57", Name = "Regular User"},
                new Role {Id = "552a17e6-6dc7-4fe0-a7a3-411f9df03011", Name = "Admin"}
            });

            dbContext.SaveChanges();
        }

        private static void SeedAdmin(ApplicationDbContext dbContext)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var adminRole = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin");

            if (adminRole == null)
            {
                return;
            }

            var admin = new User
            {
                Email = "admin@pizzatown.com",
                UserName = "admin",
                RoleId = adminRole.Id,
                PasswordHash = "$2y$10$ALZnr8OoNwvDwaWEwH/OBOpAkqZwrdti04vXdiwPDwRx7FrtXhXAW" // raw - admin123@
            };

            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
        }

        private static void SeedCategories(ApplicationDbContext dbContext)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            dbContext.Categories.AddRange(new[]
            {
                new Category {Id = new Guid(), Name = "Pizza"},
                new Category {Id = new Guid(), Name = "Burger"},
                new Category {Id = new Guid(), Name = "Drinks"},
                new Category {Id = new Guid(), Name = "Pasta"},
                new Category {Id = new Guid(), Name = "Desserts"}
            });

            dbContext.SaveChanges();
        }
    }
}