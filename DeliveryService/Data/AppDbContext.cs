using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<DeliveryRequest> DeliveryRequests { get; set; }
}