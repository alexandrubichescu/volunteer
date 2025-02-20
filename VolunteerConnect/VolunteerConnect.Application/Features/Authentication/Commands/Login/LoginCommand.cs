using MediatR;
using VolunteerConnect.Application.Features.Authentication.DTOs;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Login;

public class LoginCommand : IRequest<LoginCommandResponse>
{
    public LoginRequestDTO LoginRequestDTO { get; set; } = new();
}
