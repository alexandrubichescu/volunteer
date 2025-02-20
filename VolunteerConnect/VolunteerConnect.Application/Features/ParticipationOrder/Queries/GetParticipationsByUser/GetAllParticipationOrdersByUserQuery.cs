using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetParticipationsByUser;

public class GetAllParticipationOrdersByUserQuery: IRequest<List<ParticipationOrderListByUserVm>>
{
    public Guid Id { get; set; }
}
