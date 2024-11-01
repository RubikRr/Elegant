using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL;

public sealed class EfCoreDbContext : DbContext
{
    public DbSet<Product> Products { get; init; }
    public DbSet<Cart> Carts { get; init; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; init; }
    public DbSet<ImageItem> ImageItems { get; init; }
    public DbSet<Order> Orders { get; init; }
    public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var guidProduct = Guid.NewGuid();
        var product = new Product
        {
            Id = guidProduct,
            Name = "Пиджак",
            Cost = 3750.50m,
            Description = "Крутой пиджак для крутой леди",
            ImagePath = "/images/products/image1.png"

        };
        modelBuilder
            .Entity<Product>()
            .HasData(product);

        modelBuilder
            .Entity<ImageItem>()
            .HasData(new ImageItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = guidProduct,
                    ImagePath = "/images/products/image1.png",
                },
                new ImageItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = guidProduct,
                    ImagePath = "/images/products/image2.png",
                });
    }
}