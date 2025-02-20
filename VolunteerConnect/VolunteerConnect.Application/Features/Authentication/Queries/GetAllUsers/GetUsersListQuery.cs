using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VolunteerConnect.Application.Features.Authentication.Queries.GetAllUsers;

public class GetUsersListQuery: IRequest<List<UserVm>>
{
}
