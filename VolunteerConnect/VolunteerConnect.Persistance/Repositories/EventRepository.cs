using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Persistance.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(VolunteerConnectDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsEventTitleAndDateUnique(string name, DateTime eventDate)
        {
            var matches = _dbContext.Events.Any(e => e.Title.Equals(name) && e.Date.Date.Equals(eventDate.Date));
            return Task.FromResult(matches);
        }
    }
}
