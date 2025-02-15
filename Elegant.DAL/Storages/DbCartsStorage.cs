using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages;

public class DbCartsStorage : ICartsStorage
{
    private readonly EfCoreDbContext _dbContext;
    public DbCartsStorage(EfCoreDbContext efCoreDbContext)
    {
        _dbContext = efCoreDbContext;
    }

    public void Add(Guid userId, Product product)
    {
        var cart = _dbContext.Carts.FirstOrDefault(cart => cart.UserId == userId);
        if (cart == null)
        {
            var newCart = new Cart
            {
                UserId = userId,
            };
            newCart.Items = new List<CartOrder>
            {
                new CartOrder()
                {
                    Quantity=1,
                    Product=product
                }
            };
            _dbContext.Carts.Add(newCart);
        }
        else
        {
            var existingCartItem = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
            if (existingCartItem == null)
            {
                cart.Items.Add(new CartOrder() { Quantity = 1, Product = product });
            }
            else
            {
                existingCartItem.Quantity++;
            }
        }
        _dbContext.SaveChanges();
    }

    public void Change(Guid cartId, Guid productId, string act)
    {
        var cart = TryGetById(cartId);
        var cartItem = cart.Items.FirstOrDefault(item => item.Product.Id == productId);
        if (cartItem == null) { return; }
        if (act == "increase")
        {
            cartItem.Quantity++;
        }
        else if (act == "decrease")
        {
            if (cartItem.Quantity == 1 || cartItem.Quantity == 0)
            {
                cart.Items.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
            }
        }
        _dbContext.SaveChanges();
    }
    public Cart TryGetByUserId(Guid userId)
    {
        try
        {
            return _dbContext
                .Carts
                .Include(cart => cart.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(cart => cart.UserId == userId) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
        }

        return new Cart();
    }

    public Cart TryGetById(Guid cartId)
    {
        return _dbContext
            .Carts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.Product)
            .FirstOrDefault(cart => cart.Id == cartId) ?? throw new InvalidOperationException();
    }

    public void Clear(Guid userId)
    {
        var cart = TryGetByUserId(userId);
        cart.Items.Clear();
        _dbContext.SaveChanges();
    }

    public void Destroy(Guid userId)
    {
        var cart = TryGetByUserId(userId);
        _dbContext.Carts.Remove(cart);
        _dbContext.SaveChanges();
    }

}