using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerConnect.Application.Features.Events.Commands.CreateEvent;
using VolunteerConnect.Application.Features.Events.Commands.DeleteEvent;
using VolunteerConnect.Application.Features.Events.Commands.UpdateEvent;
using VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;
using VolunteerConnect.Application.Features.Events.Queries.GetEventsList;

namespace VolunteerConnect.Api.Controllers;
/// <summary>
/// Events Controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventsController : Controller
{
    private readonly IMediator _mediator;
    /// <summary>
    /// Events Controller Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Returns all Events
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin,User")]
    [HttpGet(Name = "GetAllEvents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<EventListVm>>> GetAllEvents()
    {
        var result = await _mediator.Send(new GetEventsListQuery());
        return Ok(result);
    }
    /// <summary>
    /// Returns a single Event by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin,User")]
    [HttpGet("{id}", Name = "GetEventById")]
    public async Task<ActionResult<EventDetailVm>> GetEventById(Guid id)
    {
        var getEventDetailQuery = new GetEventDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getEventDetailQuery));
    }

    /// <summary>
    /// Creates a new Event
    /// </summary>
    /// <param name="createEventCommand"></param>
    /// <returns></returns>
    [Authorize(Roles ="Admin")]
    [HttpPost(Name = "AddEvent")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
    {
        var id = await _mediator.Send(createEventCommand);
        return Ok(id);
    }

    /// <summary>
    /// Updates an existing Event
    /// </summary>
    /// <param name="updateEventCommand"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPut(Name = "UpdateEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
    {
        await _mediator.Send(updateEventCommand);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing Event
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}", Name = "DeleteEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleteEventCommand = new DeleteEventCommand() { EventId = id };
        await _mediator.Send(deleteEventCommand);
        return NoContent();
    }

}
