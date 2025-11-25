using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Data;
using OrderService.Models.Profiles;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем MassTransit
builder.Services.AddMassTransit(x =>
{
    // Конфигурация RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Автоматическая конфигурация endpoints
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<OrdersService>();
builder.Services.AddScoped<ProductsService>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();