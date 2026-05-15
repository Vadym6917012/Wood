
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wood.Application.Features.Products.Queries;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Отримати список товарів з фільтрацією</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? categoryId,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool? inStock,
            CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetProductsQuery(categoryId, search, sortBy, inStock), ct);
            return Ok(result);
        }

        /// <summary>Отримати рекомендовані товари</summary>
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetFeaturedProductsQuery(), ct);
            return Ok(result);
        }

        /// <summary>Отримати категорії</summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCategoriesQuery(), ct);
            return Ok(result);
        }

        /// <summary>Отримати товар за ID</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

    }
}
