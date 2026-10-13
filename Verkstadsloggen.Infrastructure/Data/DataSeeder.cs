using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Verkstadsloggen.Domain.Models;
using Verkstadsloggen.Infrastructure.Identity;

namespace Verkstadsloggen.Infrastructure.Data
{

    //Moved the seeding logic to a separate class for better organization and maintainability
    public static class DataSeeder
    {
        private static string[] Roles = { "Admin", "Mechanic", "Manager", "Customer" };

        //roles and admin
        public static async Task SeedIdentityAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }

            var adminEmail = "admin@verkstad.local";
            if (await userManager.FindByEmailAsync(adminEmail) is null) // Create admin user if it doesn't exist
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123!3");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }


        //generates test data for mechanics and customers.
        public static async Task SeedTestDataAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var names = new[]
            {
                ("Anna", "Andersson"), ("Bjorn", "Berg"), ("Cecilia", "Carlsson"), ("David", "Dahl"),
                ("Elin", "Ekström"), ("Filip", "Forsberg"), ("Greta", "Gustafsson"), ("Henrik", "Holm"),
                ("Ida", "Isaksson"), ("Johan", "Jönsson"), ("Karin", "Karlsson"), ("Lars", "Lindberg"),
                ("Maria", "Magnusson"), ("Nils", "Nilsson"), ("Olivia", "Olsson"), ("Per", "Persson"),
                ("Quentin", "Qvist"), ("Rebecka", "Rydberg"), ("Sven", "Svensson"), ("Tove", "Törnqvist")
            };

            for (int i = 0; i < names.Length; i++)
            {
                var (first, last) = names[i];
                var email = $"{first}.{last}@test.se".ToLowerInvariant()
                    .Replace("å", "a")
                    .Replace("ä", "a")
                    .Replace("ö", "o");

                if (await userManager.FindByEmailAsync(email) is not null)
                    continue;

                bool isMechanic = i < 7; // First 7 are mechanics, rest are customers
                var phone = $"070-000 00 {i + 10}";

                Person person = isMechanic
                    ? new Mechanic { FirstName = first, LastName = last, Email = email, PhoneNumber = phone }
                    : new Customer
                    {
                        FirstName = first,
                        LastName = last,
                        Email = email,
                        PhoneNumber = phone,
                        Address = $"Testgatan {i + 1}, Nyköping",
                        LicensePlates = new List<string> { $"ABC{100 + i}" }
                    };

                db.Add(person);
                await db.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumber = phone,
                    PersonId = person.Id
                };

                var result = await userManager.CreateAsync(user, "Test123!");
                if (!result.Succeeded)
                {
                    db.Remove(person);               // inget konto, inget spår
                    await db.SaveChangesAsync();
                    continue;
                }

                await userManager.AddToRoleAsync(user, isMechanic ? "Mechanic" : "Customer");
            }
        }
    }
}