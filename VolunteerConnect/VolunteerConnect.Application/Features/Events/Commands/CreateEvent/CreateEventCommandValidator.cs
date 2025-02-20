using FluentValidation;
using VolunteerConnect.Application.Contracts.Persistance;


namespace VolunteerConnect.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    public CreateEventCommandValidator(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;

    RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyTitle} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyTitle} must not exceed 50 characters.");

        RuleFor(p => p.Date)
            .NotEmpty().WithMessage("{PropertyTitle} is required.")
            .NotNull()
            .GreaterThan(DateTime.Now);

        RuleFor(e => e)
        .MustAsync(EventTitleAndDateUnique)
        .WithMessage("An event with the same title and date already exists.");

    RuleFor(p => p.Location)
        .NotEmpty().WithMessage("{Location} is required.")
        .MaximumLength(50).WithMessage("{Location} must not exceed 50 characters.");
    }

    private async Task<bool> EventTitleAndDateUnique(CreateEventCommand e, CancellationToken token)
    {
    return !await _eventRepository.IsEventTitleAndDateUnique(e.Title!, e.Date);
    }
}