﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetAllParticipationOrders;

public class GetAllParticipationOrdersQuery: IRequest<List<GetAllParticipationOrdersVm>>
{
}
