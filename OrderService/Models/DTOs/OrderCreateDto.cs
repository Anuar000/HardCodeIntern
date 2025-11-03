using OrderService.Models.DTOs;

public class OrderCreateDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItemCreateDto> OrderItems { get; set; } = new();
}
