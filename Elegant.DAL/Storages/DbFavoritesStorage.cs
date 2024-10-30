using Elegant.DAL.Interfaces;
using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages
{
    public class DbFavoritesStorage : IFavoritesStorage
    {
        private DatabaseContext dbContext;

        public DbFavoritesStorage(DatabaseContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public void Add(int userId, Product product)
        {
            var existingProduct = TryGetByUserIdAndProductId(userId, product.Id);
            if (existingProduct == null)
            {
                var newFavoriteProduct = new FavoriteProduct
                {
                    UserId = userId,
                    Product = product,
                };
                dbContext.FavoriteProducts.Add(newFavoriteProduct);
                dbContext.SaveChanges();
            }

        }

        public List<Product> GetAllProducts(int userId)
        {
            return dbContext.FavoriteProducts.Where(fav => fav.UserId == userId).Include(fav => fav.Product).Select(fav => fav.Product).ToList();
        }

        public void Clear(int userId)
        {
            var userFavoriteProducts = dbContext.FavoriteProducts.Where(fav => fav.UserId == userId).ToList();
            dbContext.FavoriteProducts.RemoveRange(userFavoriteProducts);
            dbContext.SaveChanges();
        }

        public void Remove(int userId, Guid productId)
        {
            var existingProduct = TryGetByUserIdAndProductId(userId, productId);
            dbContext.FavoriteProducts.Remove(existingProduct);
            dbContext.SaveChanges();
        }

        public FavoriteProduct TryGetByUserIdAndProductId(int userId, Guid productId)
        {
            return dbContext.FavoriteProducts.FirstOrDefault(fav => fav.UserId == userId && fav.Product.Id == productId);

        }
    }
}
