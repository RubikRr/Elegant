using Elegant.Core.Models;

namespace Elegant.DAL.Interfaces;

public interface ICartsStorage
{
    public void Add(Guid userId, Product product);

    public Cart TryGetByUserId(Guid userId);

    public Cart TryGetById(Guid cartId);

    public void Clear(Guid userId);

    public void Change(Guid cartId, Guid productId, string act);

    public void Destroy(Guid userId);

}