using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages;
public class DbProductsStorage : IProductsStorage
{
    private readonly EfCoreDbContext _dbContext;

    public DbProductsStorage(EfCoreDbContext efCoreDbContext)
    {
        _dbContext = efCoreDbContext;
    }

    public List<Product> GetAll() => _dbContext
        .Products
        .Include(product => product.CartItems)
        .Include(product => product.ImageItems)
        .ToList();
    public void Add(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }
    public void Update(Product product)
    {
        var productInStorage = TryGetById(product.Id);
        productInStorage.Name = product.Name;
        productInStorage.Cost = product.Cost;
        productInStorage.Description = product.Description;
        productInStorage.ImagePath = product.ImagePath;
        _dbContext.SaveChanges();
    }
    public List<Product> Search(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<Product>();
        return _dbContext.Products.Where(product => product.Name.ToLower().StartsWith(name.ToLower())).ToList();
    }
    public void Remove(Guid productId)
    {
        var product = TryGetById(productId);
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
    public Product TryGetById(Guid id)
    {
        return _dbContext.Products.Include(product => product.CartItems).Include(product => product.ImageItems).FirstOrDefault(pr => pr.Id == id);
    }
}