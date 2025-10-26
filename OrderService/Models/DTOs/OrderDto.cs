namespace OrderService.Models.DTOs
{
    public class OrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public ProductDto Product { get; set; }
    }
}
