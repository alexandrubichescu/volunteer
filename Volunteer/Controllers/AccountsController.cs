using Application.Features.Accounts.Queries;
using Application.Features.Accounts.Queries.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Volunteer.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private IMediator Mediator { get; set; }

    public AccountsController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        return Ok(await Mediator.Send(new GetAllAccountsQuery()));
    }
}
