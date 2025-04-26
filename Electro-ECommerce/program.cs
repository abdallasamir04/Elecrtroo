using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Electro_ECommerce.Models;
using Electro_ECommerce.Repositories;
using Electro_ECommerce.Data;
using Microsoft.Extensions.FileProviders;

using Microsoft.Extensions.FileProviders;
using System.IO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext with SQL Server
builder.Services.AddDbContext<TechXpressDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<TechXpressDbContext>()
.AddDefaultTokenProviders();

// Register the generic repository for all models
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Registering for all models like Product, Category, etc.

// Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register Session services
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




var app = builder.Build();

// Set Stripe API Key
Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];




// Configure the HTTP request pipeline.
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
//********
app.UseSession();





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


