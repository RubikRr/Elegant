using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;

namespace Elegant.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private IProductsStorage productsStorage;
        private IFavoritesStorage favoritesStorage;

        public FavoritesController(IFavoritesStorage _favoritesStorage, IProductsStorage _productsStorage)
        {
            favoritesStorage = _favoritesStorage;
            productsStorage = _productsStorage;
        }
        public IActionResult Index()
        {
            var favoriteProducts = favoritesStorage.GetAllProducts(Constants.UserId);
            return View(Mapping.ToProductsViewModel(favoriteProducts));
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            if (product == null) { return RedirectToAction("Index"); } 
            favoritesStorage.Add(Constants.UserId, product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            favoritesStorage.Remove(Constants.UserId, productId);
            return RedirectToAction("Index");
        }
    }
}
