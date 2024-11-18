using INUPCO.Catalog.Application.DTOs;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.ApproveGenericItem;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.CreateGenericItem;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.RejectGenericItem;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CursrorAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericItemPharmaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenericItemPharmaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new Generic Item Pharma
        /// </summary>
        /// <param name="command">The creation command</param>
        /// <returns>The created item</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GenericItemPharmaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenericItemPharmaDto>> Create(CreateGenericItemCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Approves a Generic Item Pharma
        /// </summary>
        /// <param name="id">The item ID</param>
        /// <param name="command">The approval command</param>
        [HttpPut("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Approve(int id, ApproveGenericItemCommand command)
        {
            command = command with { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/reject")]
        public async Task<ActionResult> Reject(int id, RejectGenericItemCommand command)
        {
            command = command with { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Gets a Generic Item Pharma by ID
        /// </summary>
        /// <param name="id">The item ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenericItemPharmaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenericItemPharmaDto>> GetById(int id)
        {
            var query = new GetGenericItemPharmaByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 