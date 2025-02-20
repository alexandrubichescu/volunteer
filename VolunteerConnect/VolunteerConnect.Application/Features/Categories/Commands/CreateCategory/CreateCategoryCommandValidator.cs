using FluentValidation;

namespace VolunteerConnect.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyTitle} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyTitle} must not exceed 50 characters.");
        RuleFor(p => p.Description)
           .NotEmpty().WithMessage("{PropertyDescription} is required.")
           .NotNull()
           .MaximumLength(1500).WithMessage("{PropertyDescription} must not exceed 1500 characters.");
        RuleFor(p => p.ImageUrl)
           .NotEmpty().WithMessage("{PropertyImageUrl} is required.")
           .NotNull()
           .MaximumLength(500).WithMessage("{PropertyImageUrl} must not exceed 500 characters.");
    }
}
