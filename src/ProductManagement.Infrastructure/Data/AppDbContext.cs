using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Entities;

namespace ProductManagement.Infrastructure.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entities here
        modelBuilder.Entity<Product>()
            .Property(p => p.QuantityPerUnit)
            .HasConversion<string>();

        // Add other configurations as needed
    }
}

