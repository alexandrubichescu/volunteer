using MediatR;
using VolunteerConnect.Application.Contracts.Authentication;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new RegisterCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new RegisterCommandResponse
            {
                Success = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        // Process the registration
        var registerDto = request.RegisterRequestDto;

        var (success, userId, errors) = await _authenticationService.RegisterUserAsync(
            registerDto.Email, registerDto.Password, registerDto.FirstName, registerDto.LastName);

        if (!success)
        {
            return new RegisterCommandResponse
            {
                Success = false,
                Errors = errors
            };
        }

        return new RegisterCommandResponse
        {
            Success = true,
            Message = "User registered successfully"
        };
    }
}
