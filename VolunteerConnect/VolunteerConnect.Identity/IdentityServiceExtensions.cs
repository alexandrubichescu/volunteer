using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VolunteerConnect.Application.Contracts.Authentication;
using VolunteerConnect.Identity.Models;
using VolunteerConnect.Identity.Services;


namespace VolunteerConnect.Identity;

public static class IdentityServiceExtensions
{

    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<VolunteerConnectIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("VolunteerConnectConnectionString")));
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<VolunteerConnectIdentityDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        
        string keyString = "SuperSecureAndVeryLongSecretKey123456789!";
        var key = Encoding.UTF8.GetBytes(keyString);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "YourIssuer",
                ValidAudience = "YourAudience",
                IssuerSigningKey = new SymmetricSecurityKey(key),
                RoleClaimType = ClaimTypes.Role // Important: Ensure role claims are recognized
            };
        });



        // Add authentication and authorization
        services.AddAuthentication();
        services.AddAuthorization();




    }
    public static async Task SeedIdentityData(this IServiceProvider serviceProvider)
    {
        await Identity.IdentitySeeder.SeedRolesAndAdmin(serviceProvider);
    }

}
