using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<ImageItem> ImageItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var guidProduct = Guid.NewGuid();
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = guidProduct,
                Name = "Пиджак",
                Cost = 3750.50m,
                Description = "Крутой пиджак для крутой леди",
                ImagePath = "/images/products/image1.png"
            });

            modelBuilder.Entity<ImageItem>().HasData(new ImageItem { Id = Guid.NewGuid(), ProductId = guidProduct, ImagePath = "/images/products/image1.png" }, new ImageItem { Id = Guid.NewGuid(), ProductId = guidProduct, ImagePath = "/images/products/image2.png" });
            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Пиджак",
            //        Cost = 3750.50m,
            //        Description = "Крутой пиджак для крутой леди",

            //        ImagePath = "/images/products/image1.png"
            //    },
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Платье",
            //        Cost = 5700.75m,
            //        Description = "Даже патрик обзавидуется такому платью",

            //        ImagePath = "/images/products/image2.png"
            //    },
            //    new Product
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Туфли",
            //        Cost = 3500.75m,
            //        Description = "Туфельки для красотульки",

            //        ImagePath = "/images/products/image3.png"
            //    });
        }
    }
}
