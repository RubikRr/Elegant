using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.DB.Interfaces;
using WomanShop.Areas.Admin.Models;
using WomanShop.Helpers;
using WomanShop.Models;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IProductsStorage productsStorage;
        public ProductController(IProductsStorage _productsStorage)
        {
            productsStorage = _productsStorage;
        }

        public IActionResult Index()
        {
            var products = productsStorage.GetAll();
            return View(Mapping.ToProductsViewModel(products));
        }
        public IActionResult Remove(Guid productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                productsStorage.Add(Mapping.ToProductModel(product));
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(Mapping.ToProductViewModel(product));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                productsStorage.Update(Mapping.ToProductModel(product));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Update");
        }
    }
}
