using CIPLATFORM.Entities.Data;
using CIPLATFORM.Respository.Interface;
using CIPLATFORM.Respository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CiPlatformContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
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

app.UseAuthorization();
app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Platform}/{action=HomeGrid}"
//    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=login}"
    );



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Profile}/{action=Profile}"
//    );
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Platform}/{action=ShareStory}"
//    );


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Platform}/{action=MissionListing}"
//    );



app.Run();
