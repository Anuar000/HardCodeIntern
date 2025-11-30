using Microsoft.AspNetCore.Mvc;
using OrderService.Models.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrdersService _ordersService;

        public OrderController(OrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            return await _ordersService.GetAllOrdersAsync();
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> GetOrderById(int id)
        {
            return await _ordersService.GetOrderByIdAsync(id);
        }

        [HttpPost]
        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            await _ordersService.CreateOrderAsync(orderCreateDto);
        }

        [HttpPut("{id}")]
        public async Task UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            await _ordersService.UpdateOrderAsync(id, orderUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteOrder(int id)
        {
            await _ordersService.DeleteOrderAsync(id);
        }
    }
}