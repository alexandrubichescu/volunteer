using MediatR;

namespace VolunteerConnect.Application.Features.Events.Queries.GetEventsList;
public class GetEventsListQuery : IRequest<List<EventListVm>>
{
}
