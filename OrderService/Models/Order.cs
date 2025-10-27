namespace OrderService.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}