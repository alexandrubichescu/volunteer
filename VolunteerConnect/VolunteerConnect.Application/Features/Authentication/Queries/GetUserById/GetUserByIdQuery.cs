using MediatR;
using VolunteerConnect.Application.Features.Authentication.Queries.GetAllUsers;

namespace VolunteerConnect.Application.Features.Authentication.Queries.GetUserById
{
    public class GetUserByIdQuery: IRequest<UserVm>
    {
        public Guid Id { get; set; }

    }
}
