using LostMap.BLL.Auth;
using LostMap.BLL.Auth.Interfaces;
using LostMap.BLL.Auth.Services;
using LostMap.BLL.Interfaces;
using LostMap.BLL.Services;
using LostMap.DAL;
using LostMap.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using static LostMap.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString(ConnectionStrings.Database);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ILossService, LossService>();
builder.Services.AddTransient<IFindingService, FindingService>();
builder.Services.AddTransient<IUserService, UserService>();

var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
builder.Services.AddSingleton(jwtConfig);

var authConfig = builder.Configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
builder.Services.AddSingleton(authConfig);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = jwtConfig.ValidationParameters;
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();