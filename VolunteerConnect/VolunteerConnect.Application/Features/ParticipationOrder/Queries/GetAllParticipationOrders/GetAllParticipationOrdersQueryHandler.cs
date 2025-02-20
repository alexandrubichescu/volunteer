using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesList;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetAllParticipationOrders
{
    public class GetAllParticipationOrdersQueryHandler : IRequestHandler<GetAllParticipationOrdersQuery, List<GetAllParticipationOrdersVm>>
    {
        public readonly IMapper _mapper;
        public readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationOrderRepository;

        public GetAllParticipationOrdersQueryHandler(IMapper mapper, IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationOrderRepository)
        {
            _mapper = mapper;
            _participationOrderRepository= participationOrderRepository;
        }

        public async Task<List<GetAllParticipationOrdersVm>> Handle(GetAllParticipationOrdersQuery request, CancellationToken cancellationToken)
        {
            var allParticipationOrders = (await _participationOrderRepository.ListAllAsync()).OrderBy(x => x.Status);
            var allParticipationOrdersVm = _mapper.Map<List<GetAllParticipationOrdersVm>>(allParticipationOrders);
            return allParticipationOrdersVm;
        }
    }
}
