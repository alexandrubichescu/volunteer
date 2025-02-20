using MediatR;
using VolunteerConnect.Application.Contracts.Persistance;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.DeleteParticipationOrder;

public class DeleteParticipationOrderCommandHandler:IRequestHandler<DeleteParticipationOrderCommand>
{
    private readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationOrderRepository;
    public DeleteParticipationOrderCommandHandler(IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationOrderRepository)
    {
        _participationOrderRepository=participationOrderRepository;
    }
    public async Task Handle(DeleteParticipationOrderCommand request, CancellationToken cancellationToken)
    {
        var ParticipationOrderToDelete = await _participationOrderRepository.GetByIdAsync(request.Id);
        await _participationOrderRepository.DeleteAsync(ParticipationOrderToDelete);
        
    }


}
