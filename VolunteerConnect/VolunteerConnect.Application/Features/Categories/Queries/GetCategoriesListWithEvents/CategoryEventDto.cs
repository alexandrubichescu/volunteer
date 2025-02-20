using System;

namespace VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class CategoryEventDto
    {
        public Guid EventId { get; set; }
        public string? Title { get; set; }
        public string? CompanyHolder { get; set; }
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
