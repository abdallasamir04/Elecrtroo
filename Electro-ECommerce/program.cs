using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Data;
using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories; // Important to have this!

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<TechXpressDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity services
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<TechXpressDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Scoped Admin Seeder service
builder.Services.AddScoped<AdminSeeder>();

// ⚠️ This is critical:
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var adminSeeder = services.GetRequiredService<AdminSeeder>();
        await adminSeeder.SeedAdminUserAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
