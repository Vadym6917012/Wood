using AutoMapper;
using Wood.Application.DTOs;
using Wood.Domain.Entities;

namespace Wood.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name));

            // Category
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.ProductCount, o => o.MapFrom(s => s.Products.Count));

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));

            CreateMap<Order, OrderSummaryDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>();
        }

    }
}
