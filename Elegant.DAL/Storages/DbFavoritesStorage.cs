using Elegant.DAL.Interfaces;
using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages;

public class DbFavoritesStorage : IFavoritesStorage
{
    private readonly EfCoreDbContext _dbContext;

    public DbFavoritesStorage(EfCoreDbContext dbContext)
    {
        _dbContext = dbContext;
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
            _dbContext.FavoriteProducts.Add(newFavoriteProduct);
            _dbContext.SaveChanges();
        }

    }

    public List<Product> GetAllProducts(int userId)
    {
        return _dbContext.FavoriteProducts.Where(fav => fav.UserId == userId).Include(fav => fav.Product).Select(fav => fav.Product).ToList();
    }

    public void Clear(int userId)
    {
        var userFavoriteProducts = _dbContext.FavoriteProducts.Where(fav => fav.UserId == userId).ToList();
        _dbContext.FavoriteProducts.RemoveRange(userFavoriteProducts);
        _dbContext.SaveChanges();
    }

    public void Remove(int userId, Guid productId)
    {
        var existingProduct = TryGetByUserIdAndProductId(userId, productId);
        _dbContext.FavoriteProducts.Remove(existingProduct);
        _dbContext.SaveChanges();
    }

    public FavoriteProduct TryGetByUserIdAndProductId(int userId, Guid productId)
    {
        return _dbContext
            .FavoriteProducts
            .FirstOrDefault(fav => fav.UserId == userId && fav.Product.Id == productId) ?? throw new InvalidOperationException();

    }
}