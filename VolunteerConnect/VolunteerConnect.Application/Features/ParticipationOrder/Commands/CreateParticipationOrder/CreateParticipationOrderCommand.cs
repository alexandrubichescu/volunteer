using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder;

public class CreateParticipationOrderCommand: IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
}
