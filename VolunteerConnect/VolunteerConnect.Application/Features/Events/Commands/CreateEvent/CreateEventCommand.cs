using MediatR;

namespace VolunteerConnect.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommand : IRequest<Guid>
{
    public string? Title { get; set; }
    public string? CompanyHolder { get; set; }
    public string? Location { get; set; }
    public DateTime Date { get; set; }
    public int MaxParticipants { get; set; } // Maximum number of volunteers allowed.
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
    public override string ToString()
    {
        return $"Event title: {Title}; CompanyHolder: {CompanyHolder}; Location: {Location}; On: {Date.ToShortDateString()}; MaxParticipants: {MaxParticipants};  Description: {Description}";
    }

}
