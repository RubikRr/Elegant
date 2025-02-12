using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface IProductsStorage
{
    public Task<List<Product>> GetAll(CancellationToken cancellationToken);
    public Task<Product?> GetById(Guid id, CancellationToken cancellationToken);
    public Task Remove(Guid productId, CancellationToken cancellationToken);
    public Task Add(Product product, CancellationToken cancellationToken);
    public Task Update(Product product, CancellationToken cancellationToken);
    public Task<List<Product>> Search(string name, CancellationToken cancellationToken);
}