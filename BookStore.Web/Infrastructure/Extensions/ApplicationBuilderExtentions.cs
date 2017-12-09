using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookStore.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<BookStoreDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.AdministratorRole;

                        var roles = new[]
                        {
                            WebConstants.AdministratorRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminUser = await userManager.FindByEmailAsync(adminName);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = "admin@mysite.com",
                                FirstName = adminName,
                                LastName = "Boss",
                                UserName = adminName,
                                RegistrationDate = DateTime.UtcNow
                            };

                            await userManager.CreateAsync(adminUser, "admin12");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
