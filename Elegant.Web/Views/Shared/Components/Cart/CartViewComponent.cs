using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsStorage _cartsStorage;

        public CartViewComponent(ICartsStorage cartsStorage)
        {
            _cartsStorage = cartsStorage;
        }

        public IViewComponentResult Invoke()
        {
            var cart = Mapping.ToCartViewModel(_cartsStorage.TryGetByUserId(DbConstants.UserId));


            if (cart != null && cart.Quantity != 0) { return View("Cart", cart.Quantity.ToString()); }

            return View("Cart", "");
            ;
        }
    }
}
