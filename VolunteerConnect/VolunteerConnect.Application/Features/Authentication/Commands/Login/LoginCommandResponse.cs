using VolunteerConnect.Application.Features.Categories.Commands.CreateCategory;
using VolunteerConnect.Application.Responses;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Login;

public class LoginCommandResponse:BaseResponse
{
    public string? Token { get; set; }
    public string? UserId { get; set; }
    public string? Role { get; set; }
    public string? Username { get; set; }



    public List<string> Errors { get; set; } = new();

    public LoginCommandResponse() : base()
    {

    }

}
