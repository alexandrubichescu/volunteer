using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerConnect.Application.Features.Authentication.Commands.Login;
using VolunteerConnect.Application.Features.Authentication.Commands.Logout;
using VolunteerConnect.Application.Features.Authentication.Commands.Register;
using VolunteerConnect.Application.Features.Authentication.Queries.GetAllUsers;
using VolunteerConnect.Application.Features.Authentication.Queries.GetUserById;

/// <summary>
/// Account Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Account Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// User Registration
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var response = await _mediator.Send(command);

        if (!response.Success)
        {
            return BadRequest(new { success = false, errors = response.Errors });
        }

        return Ok(new { success = true, message = response.Message });
    }


    /// <summary>
    /// User Login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await _mediator.Send(command);

        if (!response.Success)
        {
            return Unauthorized(new { success = false, message = response.Message, errors = response.Errors });
        }


        return Ok(new
        {
            Success = true,
            token = response.Token,
            userId = response.UserId,
            role = response.Role,
            message = response.Message,
            userName=response.Username
        });
    }

    /// <summary>
    /// User Logout
    /// </summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var response = await _mediator.Send(new LogoutCommand());

        if (!response.Success)
        {
            return BadRequest("Logout failed.");
        }

        return Ok(response.Message);
    }
    /// <summary>
    /// Retrieves all users
    /// </summary>
    /// <returns>Returns all users</returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("all", Name = "GetAllUsers")]
    public async Task<ActionResult<List<UserVm>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetUsersListQuery());
        return Ok(users);
    }

    /// <summary>
    /// Gets user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>returns a user by id</returns>
    //[Authorize(Roles = "Admin,User")]
    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserVm>> GetUserById(Guid id)
    {
        var getUserByIdQuery = new GetUserByIdQuery() { Id = id };
        return Ok(await _mediator.Send(getUserByIdQuery));
    }

}
