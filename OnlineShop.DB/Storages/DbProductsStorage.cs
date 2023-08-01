using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;


namespace OnlineShop.DB.Storages
{
    public class DbProductsStorage : IProductsStorage
    {
        private DatabaseContext dbContext;
       
        public DbProductsStorage(DatabaseContext _databaseContext)
        {
            dbContext = _databaseContext;
        }

        public List<Product> GetAll() => dbContext.Products.Include(product=>product.CartItems).ToList();
        public void Add(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }
        public void Update(Product product)
        {
            var productInStorage = TryGetById(product.Id);
            productInStorage.Name = product.Name;
            productInStorage.Cost = product.Cost;
            productInStorage.Description = product.Description;
            productInStorage.ImagePath = product.ImagePath;
            dbContext.SaveChanges();
        }
        public List<Product> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Product>();
             return dbContext.Products.Where(product => product.Name.ToLower().StartsWith(name.ToLower())).ToList();
        }
        public void Remove(Guid productId)
        {
            var product=TryGetById(productId);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges() ;
        }
        public Product TryGetById(Guid id)
        {
            return dbContext.Products.Include(product=>product.CartItems).FirstOrDefault(pr => pr.Id == id);
        }
    }
}