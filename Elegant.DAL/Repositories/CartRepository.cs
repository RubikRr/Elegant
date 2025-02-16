using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Repositories;

public class CartRepository : ICartRepository
{
    private readonly EfCoreDbContext _dbContext;
    private readonly int _defaultCartOrderValue = 1;

    public CartRepository(EfCoreDbContext efCoreDbContext)
    {
        _dbContext = efCoreDbContext;
    }

    public async Task AddItemAsync(Guid userId, Product product, CancellationToken cancellationToken)
    {
        var cart = await GetByUserIdAsync(userId, cancellationToken);

        if (cart != null)
        {
            var existingCartItem = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingCartItem == null)
            {
                cart.Items.Add(new CartOrder { Product = product });
            }

            existingCartItem!.Quantity++;
        }
        else
        {
            var newItem = new CartOrder { Product = product, Quantity = _defaultCartOrderValue };
            var newCart = new Cart
            {
                UserId = userId,
                Items = [newItem]
            };

            _dbContext.Carts.Add(newCart);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Carts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.Product)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cart => cart.UserId == userId, cancellationToken);
    }

    public async Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Carts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.Id == cartId, cancellationToken);
    }

    // public void Change(Guid cartId, Guid productId, string act)
    // {
    //     var cart = TryGetById(cartId);
    //     var cartItem = cart.Items.FirstOrDefault(item => item.Product.Id == productId);
    //     if (cartItem == null)
    //     {
    //         return;
    //     }
    //
    //     if (act == "increase")
    //     {
    //         cartItem.Quantity++;
    //     }
    //     else if (act == "decrease")
    //     {
    //         if (cartItem.Quantity == 1 || cartItem.Quantity == 0)
    //         {
    //             cart.Items.Remove(cartItem);
    //         }
    //         else
    //         {
    //             cartItem.Quantity--;
    //         }
    //     }
    //
    //     _dbContext.SaveChanges();
    // }

    // public void Clear(Guid userId)
    // {
    //     var cart = TryGetByUserId(userId);
    //     cart.Items.Clear();
    //     _dbContext.SaveChanges();
    // }
    //
    // public void Destroy(Guid userId)
    // {
    //     var cart = TryGetByUserId(userId);
    //     _dbContext.Carts.Remove(cart);
    //     _dbContext.SaveChanges();
    // }

    //
    // public Task ClearItemsAsync(Guid userId)
    // {
    //     throw new NotImplementedException();
    // }
}