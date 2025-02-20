using AutoMapper;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Domain.Entities;
using MediatR;

namespace VolunteerConnect.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Domain.Entities.ParticipationOrder> _participationOrderRepository;
        private readonly IMapper _mapper;

        public DeleteEventCommandHandler(
             IMapper mapper,
             IAsyncRepository<Event> eventRepository,
             IAsyncRepository<Domain.Entities.ParticipationOrder> participationOrderRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _participationOrderRepository = participationOrderRepository;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            // Fetch the event to delete
            var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);
            if (eventToDelete == null)
            {
                throw new KeyNotFoundException($"Event with ID {request.EventId} not found.");
            }

            // Fetch all participation orders
            var allParticipationOrders = await _participationOrderRepository.ListAllAsync();

            // Filter participation orders associated with the EventId
            var participationOrdersToDelete = allParticipationOrders
                .Where(po => po.EventId == request.EventId)
                .ToList();

            // Delete all associated participation orders
            foreach (var participationOrder in participationOrdersToDelete)
            {
                await _participationOrderRepository.DeleteAsync(participationOrder);
            }

            // Delete the event
            await _eventRepository.DeleteAsync(eventToDelete);
        }
    }
}
