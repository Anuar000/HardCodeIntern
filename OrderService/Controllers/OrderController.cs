using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using OrderService.Models.DTOs;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public OrderController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        // GET: api/order/getorders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _appDbContext.Orders
                .Include(o => o.Product)
                .ToListAsync();

            if (orders.Count == 0)
                return NotFound("No orders found.");

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        // GET: api/order/getorderbyid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _appDbContext.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound($"Order with id {id} not found.");

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        // POST: api/order/createorder
        [HttpPost]
        public async Task<ActionResult<CreateOrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            // Проверяем, существует ли продукт
            var product = await _appDbContext.Products.FindAsync(createOrderDto.ProductId);
            if (product == null)
                return BadRequest($"Product with id {createOrderDto.ProductId} does not exist.");

            var order = _mapper.Map<Order>(createOrderDto);

            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();

            var createdOrderDto = _mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, createdOrderDto);
        }

        // PUT: api/order/updateorder
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            var existingOrder = await _appDbContext.Orders.FindAsync(id);
            if (existingOrder == null)
                return NotFound($"Order with id {id} not found.");

            // Проверяем существование продукта
            var product = await _appDbContext.Products.FindAsync(orderDto.ProductId);
            if (product == null)
                return BadRequest($"Product with id {orderDto.ProductId} does not exist.");

            _mapper.Map(orderDto, existingOrder);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/order/deleteorder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderToDelete = await _appDbContext.Orders.FindAsync(id);
            if (orderToDelete == null)
                return NotFound($"Order with id {id} not found.");

            _appDbContext.Orders.Remove(orderToDelete);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
