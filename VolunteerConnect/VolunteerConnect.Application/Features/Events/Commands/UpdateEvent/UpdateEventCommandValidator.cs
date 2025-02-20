using FluentValidation;

namespace VolunteerConnect.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyTitle} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyTitle} must not exceed 50 characters.");

            RuleFor(p => p.Location)
                .NotEmpty().WithMessage("{PropertyTitle} is required.");
        }
    }
}
