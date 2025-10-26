namespace OrderService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Навигационное свойство — список заказов, где этот товар используется
        public ICollection<Order> Orders { get; set; }
    }
}
