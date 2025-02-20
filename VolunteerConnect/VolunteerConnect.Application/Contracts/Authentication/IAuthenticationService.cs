using VolunteerConnect.Application.Features.Authentication.Queries;

namespace VolunteerConnect.Application.Contracts.Authentication;

public interface IAuthenticationService
{
    Task<(bool Success, string UserId, List<string> Errors)> RegisterUserAsync(string email, string password, string firstName, string lastName);
    Task<(bool Success, string Token, string UserId, string Role, string UserName, List<string> Errors)> LoginUserAsync(string email, string password);
    Task<(bool Success, string Message)> LogoutAsync();
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetByIdAsync(Guid id);
    
}
