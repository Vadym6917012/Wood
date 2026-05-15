using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Application.Common.Interfaces;
using Wood.Application.DTOs;
using Wood.Domain.Entities;

namespace Wood.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(
    string CustomerName,
    string Phone,
    string Email,
    string Address,
    string Comment,
    bool DeliveryRequired,
    List<CreateOrderItemDto> Items
) : IRequest<OrderSummaryDto>;

    public record CreateOrderItemDto(int ProductId, decimal Quantity);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Ім'я обов'язкове")
                .MaximumLength(200);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Телефон обов'язковий")
                .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
                .WithMessage("Невірний формат телефону");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Невірний формат email");

            RuleFor(x => x.Address)
                .NotEmpty().When(x => x.DeliveryRequired)
                .WithMessage("Адреса обов'язкова при доставці");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Кошик не може бути порожнім");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).GreaterThan(0);
                item.RuleFor(x => x.Quantity).GreaterThan(0);
            });
        }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderSummaryDto>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OrderSummaryDto> Handle(
            CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerName = request.CustomerName,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                Comment = request.Comment,
                DeliveryRequired = request.DeliveryRequired,
            };

            decimal total = 0;

            foreach ( var itemDto in request.Items )
            {
                var product = await _productRepo.GetByIdAsync(itemDto.ProductId, cancellationToken)
                    ?? throw new KeyNotFoundException($"Товар з ID {itemDto.ProductId} не знайдено");

                var unitPrice = product.Unit == "м³"
                    ? product.PricePerCubicMeter
                    : product.PricePerPiece;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = itemDto.Quantity,
                    Unit = product.Unit,
                    UnitPrice = unitPrice,
                };

                order.Items.Add(orderItem);
                total += orderItem.TotalPrice;
            }

            order.TotalAmount = total;

            await _orderRepo.AddAsync(order, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return _mapper.Map<OrderSummaryDto>(order);
        }
    }

}
