using Elegant.DAL.Interfaces;
using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages
{
    public class DbCartsStorage : ICartsStorage
    {

        private readonly DatabaseContext dbContext;
        public DbCartsStorage(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;

        }
        public void Add(int userId, Product product)
        {
            var cart = dbContext.Carts.FirstOrDefault(cart => cart.UserId == userId);
            if (cart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem>
                {
                    new CartItem()
                    {
                        Quantity=1,
                        Product=product
                    }
                };
                dbContext.Carts.Add(newCart);
            }
            else
            {
                var existingCartItem = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
                if (existingCartItem == null)
                {
                    cart.Items.Add(new CartItem() { Quantity = 1, Product = product });
                }
                else
                {
                    existingCartItem.Quantity++;
                }
            }
            dbContext.SaveChanges();
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
            dbContext.SaveChanges();
        }
        public Cart TryGetByUserId(int userId)
        {
            return dbContext.Carts.Include(cart => cart.Items).ThenInclude(item => item.Product).FirstOrDefault(cart => cart.UserId == userId);
        }

        public Cart TryGetById(Guid cartId)
        {
            return dbContext.Carts.Include(cart => cart.Items).ThenInclude(item => item.Product).FirstOrDefault(cart => cart.Id == cartId);
        }

        public void Clear(int userId)
        {
            var cart = TryGetByUserId(userId);
            cart.Items.Clear();
            dbContext.SaveChanges();
        }

        public void Destroy(int userId)
        {
            var cart = TryGetByUserId(userId);
            dbContext.Carts.Remove(cart);
            dbContext.SaveChanges();
        }

    }
}
