namespace OrderService.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public int ProductId { get; set; }    // Внешний ключ
    public Product Product { get; set; }  // Навигационное свойство

    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}