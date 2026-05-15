using AutoMapper;
using MediatR;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;

namespace Wood.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(
            GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return product is null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}
