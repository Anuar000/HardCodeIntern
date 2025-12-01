using OrderService.Models.DTOs;

namespace OrderService.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(OrderCreateDto dto);
    Task UpdateOrderAsync(int id, OrderUpdateDto dto);
    Task DeleteOrderAsync(int id);
}