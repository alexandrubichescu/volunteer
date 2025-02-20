using MediatR;
using VolunteerConnect.Application.Features.Authentication.DTOs;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{
    public RegisterRequestDTO RegisterRequestDto { get; set; } = new();
}
