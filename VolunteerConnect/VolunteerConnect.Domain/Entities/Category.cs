
using VolunteerConnect.Domain.Common;

namespace VolunteerConnect.Domain.Entities;

public class Category : AuditableEntity
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<Event> Events { get; set; }
}
