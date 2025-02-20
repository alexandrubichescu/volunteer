using VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetParticipationsByUser;

public class ParticipationOrderListByUserVm
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public ParticipationStatusDto Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public CategoryDto? Category { get; set; }

}
