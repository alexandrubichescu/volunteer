using System;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;

public class EventDetailVm
{
    public Guid EventId { get; set; }
    public string? Title { get; set; }
    public string? CompanyHolder { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; } // Maximum number of volunteers allowed.
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}



