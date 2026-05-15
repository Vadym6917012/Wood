using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wood.Application.DTOs
{
    public record ProductDto(
    int Id,
    string Name,
    string Description,
    string Species,
    string Grade,
    string Dimensions,
    decimal PricePerCubicMeter,
    decimal PricePerPiece,
    string Unit,
    int CategoryId,
    string CategoryName,
    bool InStock,
    string ImageUrl,
    bool IsFeatured,
    DateTime CreatedAt
);

    public record CategoryDto(
        int Id,
        string Name,
        string Slug,
        string Icon,
        int ProductCount
    );

    public record OrderDto(
        int Id,
        string CustomerName,
        string Phone,
        string Email,
        string Address,
        bool DeliveryRequired,
        string Status,
        decimal TotalAmount,
        DateTime CreatedAt,
        List<OrderItemDto> Items
    );

    public record OrderItemDto(
        int Id,
        int ProductId,
        string ProductName,
        decimal Quantity,
        string Unit,
        decimal UnitPrice,
        decimal TotalPrice
    );

    public record OrderSummaryDto(
        int Id,
        string CustomerName,
        string Phone,
        string Status,
        decimal TotalAmount,
        bool DeliveryRequired,
        DateTime CreatedAt
    );

}
