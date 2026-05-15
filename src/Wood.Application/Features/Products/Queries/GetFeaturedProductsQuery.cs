using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;

namespace Wood.Application.Features.Products.Queries
{
    public record GetFeaturedProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
    public class GetFeaturedProductsQueryHandler
    : IRequestHandler<GetFeaturedProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetFeaturedProductsQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle(
            GetFeaturedProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.GetFeaturedAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
    }
}
