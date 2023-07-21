using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using WomanShop.Helpers;

namespace WomanShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public HomeController(IProductsStorage _productsStorage)
        {
            productsStorage = _productsStorage;
        }
        public IActionResult Index()
        {
            var productsModel = productsStorage.GetAll();
            var test = Mapping.ToProductsViewModel(productsModel);
            return View(test);
        }

        [HttpPost]
        public IActionResult Search(string productName)
        {   
            var productsModel=productsStorage.Search(productName);
            return View(Mapping.ToProductsViewModel(productsModel));
        }
    }
}