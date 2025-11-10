using MassTransit;

namespace OrderService.Events;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"Получен заказ: ID={message.OrderId}, Клиент={message.CustomerName}, Дата={message.OrderDate}");
    }
}