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

            MigrateDatabase(serviceProvider);

            //SeedRoles(serviceProvider);
            SeedAdmin(serviceProvider);
            SeedCategories(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        //private static void SeedRoles(IServiceProvider serviceProvider)
        //{
        //    var dbContext = serviceProvider
        //        .GetRequiredService<ApplicationDbContext>();

        //    if (dbContext.Roles.Any())
        //    {
        //        return;
        //    }

        //    dbContext.Roles.AddRange(new[]
        //    {
        //        new Role {Id = new Guid().ToString(), Name = "Regular User"},
        //        new Role {Id = new Guid().ToString(), Name = "Admin"}
        //    });

        //    dbContext.SaveChanges();
        //}

        private static void SeedAdmin(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();

            if (dbContext.Users.Any())
            {
                return;
            }

            //var adminRole = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin");

            //if (adminRole == null)
            //{
            //    return;
            //}

            var admin = new User
            {
                Email = "admin@pizzatown.com",
                UserName = "admin",
                //RoleId = Guid.Parse(adminRole.Id),
                PasswordHash = "$2y$10$ALZnr8OoNwvDwaWEwH/OBOpAkqZwrdti04vXdiwPDwRx7FrtXhXAW" // raw - admin123@
            };

            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
        }

        private static void SeedCategories(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .GetRequiredService<ApplicationDbContext>();

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