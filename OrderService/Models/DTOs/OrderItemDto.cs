namespace OrderService.Models.DTOs;

public class OrderItemDto
{
    public int Quantity { get; set; }
    public ProductDto Product { get; set; } = null!;
}