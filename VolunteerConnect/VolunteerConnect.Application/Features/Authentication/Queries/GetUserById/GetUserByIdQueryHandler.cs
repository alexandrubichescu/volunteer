using AutoMapper;
using MediatR;
using VolunteerConnect.Application.Contracts.Authentication;
using VolunteerConnect.Application.Features.Authentication.Queries.GetAllUsers;

namespace VolunteerConnect.Application.Features.Authentication.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserVm>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        mapper = _mapper;
    }
    public async Task<UserVm> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.GetByIdAsync(request.Id);

        // Map UserDto to UsersListVm
        var userDto = new UserVm
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Username
        };
        return userDto;


    }
}
