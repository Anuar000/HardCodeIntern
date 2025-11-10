using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Events;
using OrderService.Models;
using OrderService.Models.DTOs;

namespace OrderService.Services
{
    public class OrdersService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersService(AppDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Order>(orderCreateDto);

            foreach (var itemDto in orderCreateDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                    return (false, $"Product with ID {itemDto.ProductId} not found");
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            // 🔹 Отправляем событие
            var orderEvent = new OrderCreatedEvent
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate
            };

            await _publishEndpoint.Publish(orderEvent);
            
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return (false, "Order not found");

            _mapper.Map(orderUpdateDto, order);

            foreach (var itemDto in orderUpdateDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                    return (false, $"Product with ID {itemDto.ProductId} not found");
            }

            await _context.SaveChangesAsync();
            return (true, null);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
