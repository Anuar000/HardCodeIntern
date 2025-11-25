using Contracts;
using MassTransit;
using DeliveryService.Data;
using DeliveryService.Models;

namespace DeliveryService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly AppDbContext _dbContext;

        public OrderCreatedConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            var delivery = new DeliveryRequest
            {
                OrderId = message.OrderId,
                CustomerName = message.CustomerName,
                OrderCreatedAt = message.OrderDate
            };

            _dbContext.DeliveryRequests.Add(delivery);
            await _dbContext.SaveChangesAsync();
            
            Console.WriteLine("Order created");
        }
    }
}