using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Contracts.Persistance;

public interface ICategoryRepository : IAsyncRepository<Category>
{
    Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
}
