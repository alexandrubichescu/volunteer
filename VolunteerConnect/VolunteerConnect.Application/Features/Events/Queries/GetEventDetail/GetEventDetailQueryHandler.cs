﻿using AutoMapper;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Domain.Entities;
using MediatR;

namespace VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;

public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetEventDetailQueryHandler(
        IMapper mapper,
        IAsyncRepository<Event> eventRepository,
        IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);


        var eventDetailDto = _mapper.Map<EventDetailVm>(@event);

        var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

        eventDetailDto.Category = _mapper.Map<CategoryDto>(category);

        return eventDetailDto;
    }
}
