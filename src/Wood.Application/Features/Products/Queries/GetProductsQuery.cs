using AutoMapper;
using MediatR;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;

namespace Wood.Application.Features.Products.Queries
{
    public record GetProductsQuery(
        int? CategoryId,
        string? Search,
        string? SortBy,
        bool? InStock
    ) : IRequest<IReadOnlyList<ProductDto>>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle(
            GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.GetFilteredAsync(
                request.CategoryId,
                request.Search,
                request.SortBy,
                request.InStock,
                cancellationToken);

            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }

}
