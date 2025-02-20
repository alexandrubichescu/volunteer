using MediatR;
using VolunteerConnect.Application.Contracts.Authentication;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutCommandResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LogoutCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<LogoutCommandResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var (success, message) = await _authenticationService.LogoutAsync();

        return new LogoutCommandResponse
        {
            Success = success,
            Message = message
        };
    }
}
