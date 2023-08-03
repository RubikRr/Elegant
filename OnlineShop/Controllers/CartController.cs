using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Interfaces;
using WomanShop.Helpers;

namespace WomanShop.Controllers
{
    
    public class CartController:Controller
    {

        private IProductsStorage productsStorage { get; }

        private ICartsStorage cartsStorage { get; set; }

        public CartController(IProductsStorage _productsStorage,ICartsStorage _cartStorage)
        {
           productsStorage = _productsStorage ;
           cartsStorage = _cartStorage;
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            cartsStorage.Add(Constants.UserId, product);
      
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            var userCart = cartsStorage.TryGetByUserId(Constants.UserId);
           
            return View(Mapping.ToCartViewModel(userCart));
        }

        public IActionResult Clear()
        {
            
            cartsStorage.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult ChangeCount(Guid cartId,Guid productId, string act)
        {
            cartsStorage.Change(cartId, productId, act);
            return RedirectToAction("Index");
        }
    }
}
