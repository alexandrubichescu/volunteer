using MediatR;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.UpdateParticipationOrder;

public class UpdateParticipationOrderCommand: IRequest<Guid>
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public ParticipationStatusDto Status { get; set; }
}
