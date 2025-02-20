
using VolunteerConnect.Domain.Common;

namespace VolunteerConnect.Domain.Entities;

public class ParticipationOrder : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public ParticipationStatus Status { get; set; } = ParticipationStatus.Pending; // Default request status.
}
