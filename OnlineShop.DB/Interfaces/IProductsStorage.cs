using OnlineShop.DB.Models;

namespace OnlineShop.DB.Interfaces
{
    public interface IProductsStorage
    {
        public List<Product> GetAll();
        public Product TryGetById(Guid id);
        public void Remove(Guid productId);
        public void Add(Product product);
        public void Update(Product product);
        public List<Product> Search(string name);
    }
}
