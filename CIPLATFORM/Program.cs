using CIPLATFORM.Entities.Data;
using CIPLATFORM.Respository.Interface;
using CIPLATFORM.Respository.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/login";
        options.LogoutPath = "/User/Logout";
    });



// Add services to the container.
builder.Services.AddDbContext<CiPlatformContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();


builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddDbContext<CiPlatformContext>(options => options.UseSqlServer(

builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=login}"
    );





app.Run();
