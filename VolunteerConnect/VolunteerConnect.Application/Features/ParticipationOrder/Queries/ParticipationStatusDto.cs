using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Queries
{
    public enum ParticipationStatusDto
    {
        Pending,   // Request sent, waiting for admin approval.
        Approved,  // Admin approved the request.
        Rejected   // Admin rejected the request.
    }


}
