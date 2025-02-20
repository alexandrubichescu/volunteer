using MediatR;
using VolunteerConnect.Application.Contracts.Authentication;
using VolunteerConnect.Application.Features.Authentication.Queries.GetAllUsers;

public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserVm>>
{
    private readonly IAuthenticationService _authenticationService;

    public GetUsersListQueryHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<List<UserVm>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        var users = await _authenticationService.GetAllUsersAsync();

        // Map UserDto to UsersListVm
        var usersDto= users.Select(user => new UserVm
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Username
        }).ToList();
        return usersDto;
    }
}
