namespace OrderService.Models.DTOs;

public class OrderUpdateDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItemCreateDto> OrderItems { get; set; } = new();
}