using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using WomanShop.Helpers;
using WomanShop.Models;
using WomanShop.Storages;

namespace WomanShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;
        public ProductController(IProductsStorage _productsStorage)
        {
            productsStorage = _productsStorage;
        }
        public IActionResult Index(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            //return product != null? product.ToString():$"Товар с индексом {id} не существует";
            return View(Mapping.ToProductViewModel(product));
        }

    }
}
