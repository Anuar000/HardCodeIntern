using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public OrderController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public ActionResult<List<Order>> GetOrders()
    {
        var orders = _appDbContext.Orders.ToList();
        
        if (orders.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(orders);
    }
    
    [HttpGet("{id}")]
    public ActionResult<List<Order>> GetOrderById(int id)
    {
        var order = _appDbContext.Orders.Find(id);

        if (order == null)
        {
            return NotFound();
        }
        
        return Ok(order);
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        _appDbContext.Orders.Add(order);
        _appDbContext.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateOrder(Order order)
    {
        var orderToUpdate = _appDbContext.Orders.Find(order.Id);
        if (orderToUpdate == null)
        {
            return NotFound();
        }
        
        _appDbContext.Orders.Update(orderToUpdate);
        _appDbContext.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteOrder(int id)
    {
        var orderToDelete = _appDbContext.Orders.Find(id);
        
        if (orderToDelete == null)
        {
            return NotFound();
        }

        _appDbContext.Orders.Remove(orderToDelete);
        _appDbContext.SaveChanges();
        return Ok();
    }
}