using VolunteerConnect.Domain.Common;

namespace VolunteerConnect.Domain.Entities;

public enum ParticipationStatus
{
    Pending,   // Request sent, waiting for admin approval.
    Approved,  // Admin approved the request.
    Rejected   // Admin rejected the request.
}
