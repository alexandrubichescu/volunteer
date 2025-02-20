using System;
using System.Collections.Generic;

namespace VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class CategoryEventListVm
    {
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public ICollection<CategoryEventDto> CategoryEventsList { get; set; }
    }
}
