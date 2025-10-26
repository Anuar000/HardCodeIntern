public class CreateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
