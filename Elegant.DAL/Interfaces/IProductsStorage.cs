using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface IProductsStorage
{
    public Task<List<Product>> GetAll();
    public Task<Product?> GetById(Guid id);
    public Task Remove(Guid productId);
    public Task Add(Product product);
    public Task Update(Product product);
    public Task<List<Product>> Search(string name);
}