using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaTown.Data;
using PizzaTown.Data.Models;
using PizzaTown.Infrastructure;
using PizzaTown.MappingConfiguration;
using PizzaTown.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString!);
        options.EnableSensitiveDataLogging();
    }
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddScoped<RoleManager<Role>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.PrepareDatabase();
app.Run();