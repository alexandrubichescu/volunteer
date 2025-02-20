using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.DeleteParticipationOrder
{
    public class DeleteParticipationOrderCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
