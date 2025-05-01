using Microsoft.AspNetCore.Identity;
using Electro_ECommerce.Models;

namespace Electro_ECommerce.Data
{
    public class AdminSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminUserAsync()
        {
            // Create Admin role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Create Customer role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            // Check if admin user exists
            var adminUser = await _userManager.FindByEmailAsync("admin@electro.com");

            if (adminUser == null)
            {
                // Create admin user
                adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@electro.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    Name = "Admin User",
                    Role = "Admin",
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    // Add admin to Admin role
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
