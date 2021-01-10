using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach(var user in users)
            {
                using var hamc = new HMACSHA512();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hamc.ComputeHash(Encoding.UTF8.GetBytes("Passord"));
                user.PasswordSalt = hamc.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}