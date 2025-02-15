using Elegant.Business.Mapping;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartRepository _cartRepository;

        public CartViewComponent(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IViewComponentResult Invoke()
        {
            // var cart = Mapping.ToCartViewModel(_cartsStorage.TryGetByUserId(DbConstants.UserId));
            //
            //
            // if (cart.Quantity != 0) { return View("Cart", cart.Quantity.ToString()); }

            return View("Cart", "");
        }
    }
}
