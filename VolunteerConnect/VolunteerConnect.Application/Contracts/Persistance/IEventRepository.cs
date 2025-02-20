using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Contracts.Persistance;

public interface IEventRepository : IAsyncRepository<Event>
{
    Task<bool> IsEventTitleAndDateUnique(string title, DateTime eventDate);
}
