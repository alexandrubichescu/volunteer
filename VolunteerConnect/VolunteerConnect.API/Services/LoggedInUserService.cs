using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using VolunteerConnect.Application.Contracts;

namespace VolunteerConnect.Api.Services;
/// <summary>
/// Logged in user service.
/// </summary>
public class LoggedInUserService : ILoggedInUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// Logged in user service constructor.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    /// <summary>
    /// User Id.
    /// </summary>
    public string UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? string.Empty;
        }
    }
    /// <summary>
    /// User Name.
    /// </summary>
    public string UserName
    {
        get
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            return userName ?? string.Empty;
        }
    }
}
