using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetParticipationsByUser;

public class GetAllParticipationOrdersByUserIdQueryHandler : IRequestHandler<GetAllParticipationOrdersByUserQuery, List<ParticipationOrderListByUserVm>>
{
    private readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IAsyncRepository<Event> _eventRepository;

    private readonly IMapper _mapper;

    public GetAllParticipationOrdersByUserIdQueryHandler(
        IMapper mapper,
        IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationRepository,
        IAsyncRepository<Category> categoryRepository,
        IAsyncRepository<Event> eventRepository)
    {
        _mapper = mapper;
        _participationRepository = participationRepository;
        _categoryRepository = categoryRepository;
        _eventRepository= eventRepository;
    }

    public async Task<List<ParticipationOrderListByUserVm>> Handle(GetAllParticipationOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        // Fetch participation orders
        var participationOrders = await _participationRepository.ListAllAsync();

        // Filter by UserId
        var filteredList = participationOrders.Where(order => order.UserId == request.Id).ToList();

        // Fetch all events and categories
        var events = await _eventRepository.ListAllAsync();
        var categories = await _categoryRepository.ListAllAsync();

        // Map participation orders to ViewModel
        var result = _mapper.Map<List<ParticipationOrderListByUserVm>>(filteredList);

        // Match events and categories to participation orders
        foreach (var participationOrder in result)
        {
            // Find the related event
            var relatedEvent = events.FirstOrDefault(e => e.EventId == participationOrder.EventId);
            if (relatedEvent != null)
            {
                // Ensure the Category property is initialized
                if (participationOrder.Category == null)
                {
                    participationOrder.Category = new CategoryDto();
                }

                // Set category ID and title
                participationOrder.Category.Id = relatedEvent.CategoryId;

                var category = categories.FirstOrDefault(c => c.CategoryId == relatedEvent.CategoryId);
                if (category != null)
                {
                    participationOrder.Category.Title = category.Title;
                }
            }
        }

        return result;
    }


}
