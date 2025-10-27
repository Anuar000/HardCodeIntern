using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---- Order ----
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            // Связь: Order (1) → (много) OrderItem
            entity.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ---- OrderItem ----
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);

            // Связь: OrderItem (много) → (1) Product
            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---- Product ----
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
        });
    }
}