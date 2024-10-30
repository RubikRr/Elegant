using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;

namespace WomanShop.Views.Shared.Components.Cart
{
    public class CartViewComponent:ViewComponent
    {
        private ICartsStorage cartsStorage;

        public CartViewComponent(ICartsStorage _cartsStorage)
        {
            cartsStorage = _cartsStorage;
        }

        public IViewComponentResult Invoke()
        {
            var cart = Mapping.ToCartViewModel(cartsStorage.TryGetByUserId(Constants.UserId));
           

            if (cart != null && cart.Quantity!=0) { return View("Cart", cart.Quantity.ToString());  }

            return View("Cart", "");
            ;
        }
    }
}
