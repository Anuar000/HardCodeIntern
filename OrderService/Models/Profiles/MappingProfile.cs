using AutoMapper;
using OrderService.Models.DTOs;

namespace OrderService.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Product → ProductDto
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            // OrderItem → OrderItemDto
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            // OrderItemCreateDto → OrderItem
            CreateMap<OrderItemCreateDto, OrderItem>();

            // Order → OrderDto
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            // Order → OrderCreateDto
            CreateMap<Order, OrderCreateDto>();
            CreateMap<OrderCreateDto, Order>();

            // Order → OrderUpdateDto
            CreateMap<Order, OrderUpdateDto>();
            CreateMap<OrderUpdateDto, Order>();
        }
    }
}
