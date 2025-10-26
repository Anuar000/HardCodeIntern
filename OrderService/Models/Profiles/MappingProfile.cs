using AutoMapper;
using OrderService.Models.DTOs;

namespace OrderService.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Order, OrderDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<ProductDto, Product>();

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateProductDto, Product>();
        }
    }
}
