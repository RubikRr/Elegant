using Elegant.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL;

public sealed class EfCoreDbContext : DbContext
{
    public DbSet<Product> Products { get; init; }
    public DbSet<Cart> Carts { get; init; }
    public DbSet<FavoriteProduct> FavoriteProducts { get; init; }
    public DbSet<Image> ImageItems { get; init; }
    public DbSet<Order> Orders { get; init; }
    public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
    {
        Database.Migrate();
    }
}