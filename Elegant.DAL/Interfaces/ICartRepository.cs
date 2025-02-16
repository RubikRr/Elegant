using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface ICartRepository
{
    public Task AddItemAsync(Guid userId, Product product, CancellationToken cancellationToken = default);

    public Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    public Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken = default);

    // public Task ClearItemsAsync(Guid userId);

    //public void Change(Guid cartId, Guid productId, string act);

    //public void Destroy(Guid userId);

}