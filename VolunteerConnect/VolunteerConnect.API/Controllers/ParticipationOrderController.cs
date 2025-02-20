using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesList;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.DeleteParticipationOrder;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.UpdateParticipationOrder;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetAllParticipationOrders;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetParticipationsByUser;

namespace VolunteerConnect.Api.Controllers
{   
    /// <summary>
    /// Participation Order Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Participation Order Controller Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ParticipationOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Returns all Participation Orders
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("all", Name = "GetAllParticipationOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllParticipationOders()
        {
            var list = await _mediator.Send(new GetAllParticipationOrdersQuery());
            return Ok(list);
        }

        /// <summary>
        /// Returns all Participation Orders by user id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpGet("user/{id}", Name = "GetAllParticipationByUserId")]
        public async Task<ActionResult<List<ParticipationOrderListByUserVm>>> GetParticipationsByUserId(Guid id)
        {
            var participationsListByUser = new GetAllParticipationOrdersByUserQuery() { Id = id };
            var result = await _mediator.Send(participationsListByUser);
            return Ok(result);
        }

        /// <summary>
        /// Creates a participation order
        /// </summary>
        /// <param name="createParticipationOrderCommand"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin, User")]
        [HttpPost(Name = "AddParticipationOrder")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateParticipationOrderCommand createParticipationOrderCommand)
        {
            var id = await _mediator.Send(createParticipationOrderCommand);
            return Ok(id);
        }

        /// <summary>
        /// Updates a participation order
        /// </summary>
        /// <param name="updateparticipationOrderCommand"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "UpdateParticipationOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateParticipationOrderCommand updateparticipationOrderCommand)
        {
            await _mediator.Send(updateparticipationOrderCommand);
            return NoContent();
        }

        /// <summary>
        /// Deletes a participation order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}", Name = "DeleteParticipationOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteParticipationOrderCommand = new DeleteParticipationOrderCommand() { Id = id };
            await _mediator.Send(deleteParticipationOrderCommand);
            return NoContent();
        }

    }
}
