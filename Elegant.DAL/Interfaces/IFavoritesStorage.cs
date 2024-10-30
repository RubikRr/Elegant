using OnlineShop.DB.Models;

namespace Elegant.DAL.Interfaces
{
    public interface IFavoritesStorage
    {
        public void Add(int userId, Product product);

        public List<Product> GetAllProducts(int userId);

        public void Clear(int userId);

        public void Remove(int userId, Guid productId);

        public FavoriteProduct TryGetByUserIdAndProductId(int userId, Guid productId);
    }
}
