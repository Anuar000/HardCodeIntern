using OrderService.Models.DTOs;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItemDto> OrderItems { get; set; } = new();
}
