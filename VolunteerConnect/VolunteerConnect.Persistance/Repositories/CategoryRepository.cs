using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VolunteerConnect.Persistance.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(VolunteerConnectDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            return await _dbContext.Categories.Include(x => x.Events).ToListAsync();
        }
    }
}
