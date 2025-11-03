namespace OrderService.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount => OrderItems.Sum(item => item.Quantity * item.Product.Price);
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}