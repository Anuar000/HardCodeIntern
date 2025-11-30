using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Exceptions;
using OrderService.Models;
using OrderService.Models.DTOs;

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

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            throw new NotFoundException($"Order with ID {id} not found");

        return _mapper.Map<OrderDto>(order);
    }

    public async Task CreateOrderAsync(OrderCreateDto dto)
    {
        var order = _mapper.Map<Order>(dto);

        foreach (var itemDto in dto.OrderItems)
        {
            var product = await _context.Products.FindAsync(itemDto.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with ID {itemDto.ProductId} not found");
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var evt = new OrderCreatedEvent
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate
        };

        await _publishEndpoint.Publish(evt);
    }

    public async Task UpdateOrderAsync(int id, OrderUpdateDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            throw new NotFoundException($"Order with ID {id} not found");

        foreach (var itemDto in dto.OrderItems)
        {
            var product = await _context.Products.FindAsync(itemDto.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with ID {itemDto.ProductId} not found");
        }

        _mapper.Map(dto, order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            throw new NotFoundException($"Order with ID {id} not found");

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
