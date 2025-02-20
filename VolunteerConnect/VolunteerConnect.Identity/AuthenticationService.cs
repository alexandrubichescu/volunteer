using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VolunteerConnect.Application.Contracts.Authentication;
using VolunteerConnect.Application.Features.Authentication.Queries;
using VolunteerConnect.Identity.Models;

namespace VolunteerConnect.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<(bool Success, string UserId, List<string> Errors)> RegisterUserAsync(string email, string password, string firstName, string lastName)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return (false, string.Empty, result.Errors.Select(e => e.Description).ToList());
        }

        // Assign default role (User)
        await _userManager.AddToRoleAsync(user, "User");

        return (true, user.Id, new List<string>());
    }



    public async Task<(bool Success, string Token, string UserId, string Role, string UserName, List<string> Errors)> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return (false, string.Empty, string.Empty, string.Empty, string.Empty, new List<string> { "Invalid credentials" });
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles);
        var usernm=user.UserName!;

        return (true, token, user.Id, roles.FirstOrDefault() ?? "User", usernm, new List<string>());
    }




    public async Task<(bool Success, string Message)> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return (true, "Logged out successfully.");
    }


    private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecureAndVeryLongSecretKey123456789!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        // Map ApplicationUser to UserDto
        var userDtos = users.Select(user => new UserDto
        {
            Id = Guid.Parse(user.Id), // Assuming `Id` in ApplicationUser is a string
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.UserName
        }).ToList();

        return userDtos;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        // Convert Guid to string because ApplicationUser's Id is a string
        var userId = id.ToString();

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        // Map ApplicationUser to UserDto
        return new UserDto
        {
            Id = Guid.Parse(user.Id), // Convert string Id to Guid
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.UserName
        };
    }

}
