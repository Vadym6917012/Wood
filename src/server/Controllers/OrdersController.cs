using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wood.Application.Features.Orders.Commands;
using Wood.Application.Features.Orders.Queries;
using Wood.Domain.Enums;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Отримати всі замовлення</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(), ct);
            return Ok(result);
        }

        /// <summary>Отримати замовлення за ID</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Створити нове замовлення</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Оновити статус замовлення</summary>
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdateStatusRequest request,
            CancellationToken ct)
        {
            await _mediator.Send(new UpdateOrderStatusCommand(id, request.Status), ct);
            return NoContent();
        }
    }

    public record UpdateStatusRequest(OrderStatus Status);
}


