using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Application.Common.Interfaces;
using Wood.Domain.Enums;

namespace Wood.Application.Features.Orders.Commands
{
    public record UpdateOrderStatusCommand(int OrderId, OrderStatus Status) : IRequest;

    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
        }
    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IOrderRepository _repo;
        private readonly IUnitOfWork _uow;

        public UpdateOrderStatusCommandHandler(IOrderRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _repo.GetByIdAsync(request.OrderId, cancellationToken)
                ?? throw new KeyNotFoundException($"Замовлення {request.OrderId} не знайдено");

            order.Status = request.Status;
            order.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(order, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }

}
