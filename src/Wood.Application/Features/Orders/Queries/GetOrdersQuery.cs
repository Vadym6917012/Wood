using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;

namespace Wood.Application.Features.Orders.Queries
{
    public record GetAllOrdersQuery : IRequest<IReadOnlyList<OrderSummaryDto>>;

    public class GetAllOrdersQueryHandler
        : IRequestHandler<GetAllOrdersQuery, IReadOnlyList<OrderSummaryDto>>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OrderSummaryDto>> Handle(
            GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<OrderSummaryDto>>(orders);
        }
    }

    public record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(
            GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }
    }
}
