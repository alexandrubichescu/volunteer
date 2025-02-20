using MediatR;
using VolunteerConnect.Application.Contracts.Authentication;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new LoginCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new LoginCommandResponse
            {
                Success = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        // Process the login
        var loginDto = request.LoginRequestDTO;

        var (success, token, userId, role, username, errors) = await _authenticationService.LoginUserAsync(
            loginDto.Email, loginDto.Password);

        if (!success)
        {
            return new LoginCommandResponse
            {
                Success = false,
                Message = errors.FirstOrDefault() ?? "Invalid credentials",
                Errors=errors
            };
        }

        return new LoginCommandResponse
        {
            Success = true,
            Token = token,
            UserId = userId,
            Role = role,
            Username=username,
            Message="User logged in successfully"
        };
    }
}
