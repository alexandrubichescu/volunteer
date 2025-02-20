using FluentValidation;
using VolunteerConnect.Application.Contracts.Persistance;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder
{
    public class CreateParticipationOrderCommandValidator : AbstractValidator<CreateParticipationOrderCommand>
    {
        private readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationOrderRepository;

        public CreateParticipationOrderCommandValidator(IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationOrderRepository)
        {
            _participationOrderRepository = participationOrderRepository;

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{UserId} is required.");

            RuleFor(p => p.EventId)
                .NotEmpty().WithMessage("{EventId} is required.");

            RuleFor(e => e)
                .MustAsync(BeUniqueParticipationOrder)
                .WithMessage("A participation order for this user and event already exists.");
        }

        private async Task<bool> BeUniqueParticipationOrder(CreateParticipationOrderCommand command, CancellationToken cancellationToken)
        {
            var existingOrders = await _participationOrderRepository.ListAllAsync();
            return !existingOrders.Any(po => po.UserId == command.UserId && po.EventId == command.EventId);
        }
    }
}
