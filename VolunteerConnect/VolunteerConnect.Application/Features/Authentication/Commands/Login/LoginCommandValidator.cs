using FluentValidation;

namespace VolunteerConnect.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginRequestDTO.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.LoginRequestDTO.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
