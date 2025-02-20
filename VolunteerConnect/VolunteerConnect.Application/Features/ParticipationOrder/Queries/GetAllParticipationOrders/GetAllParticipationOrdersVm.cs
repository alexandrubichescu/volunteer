namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetAllParticipationOrders;

public class GetAllParticipationOrdersVm
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string? EventId { get; set; }
    public ParticipationStatusDto Status { get; set; }
}
