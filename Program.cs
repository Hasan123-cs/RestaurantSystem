using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Data;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<UserOperation>();
builder.Services.AddScoped<CategoryOperation>();
builder.Services.AddScoped<MenuItemService>();
builder.Services.AddScoped<OrderOperation>();
builder.Services.AddScoped<DashboardAdmin>();
builder.Services.AddScoped<CRUD_CATEGORY>();
builder.Services.AddScoped<CRUD_MENU>();



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<AppDbContext>(
options => options.UseNpgsql(
builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.MapGet("/", () =>
{
    PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

    string hashedPassword =
        passwordHasher.HashPassword(null, "adminHasan123");
    return hashedPassword;
});
app.Run();
