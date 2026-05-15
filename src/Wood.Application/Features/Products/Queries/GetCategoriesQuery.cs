using AutoMapper;
using MediatR;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;

namespace Wood.Application.Features.Products.Queries
{

    public record GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;

    public class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> Handle(
            GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }
    }
}
