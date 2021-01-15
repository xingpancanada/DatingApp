using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class Seed
    {
        // public static async Task SeedUsers(DataContext context)
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if(await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach(var user in users)
            {
                // using var hamc = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                // user.PasswordHash = hamc.ComputeHash(Encoding.UTF8.GetBytes("Passord"));
                // user.PasswordSalt = hamc.Key;

                // await context.Users.AddAsync(user);
                await userManager.CreateAsync(user, "Password123456");
                await userManager.AddToRoleAsync(user, "Member");
            }

            // await context.SaveChangesAsync();

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Password123456");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
        }
    }
}