using AutoMapper;
using OrderService.Models.DTOs;

namespace OrderService.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Product
            CreateMap<Product, ProductDto>().ReverseMap();

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // Order
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
        }
    }
}
