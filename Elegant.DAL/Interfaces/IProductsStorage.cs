using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface IProductsStorage
{
    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task RemoveAsync(Guid productId, CancellationToken cancellationToken);
    public Task AddAsync(Product product, CancellationToken cancellationToken);
    public Task UpdateAsync(Product product, CancellationToken cancellationToken);
    public Task<List<Product>> SearchAsync(string name, CancellationToken cancellationToken);
}