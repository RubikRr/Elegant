using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface ICartRepository
{
    public Task AddAsync(Guid userId, Product product);

    public Task<Cart> GetByUserIdAsync(Guid userId);

    public Task<Cart> GetByIdAsync(Guid cartId);

    public Task ClearItemsAsync(Guid userId);

    //public void Change(Guid cartId, Guid productId, string act);

    //public void Destroy(Guid userId);

}