namespace VolunteerConnect.Application.Features.Events.Queries.GetEventsList;

public class EventListVm
{
    public Guid EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? CompanyHolder { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}