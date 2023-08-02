using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using WomanShop.Helpers;

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
            var ans = Mapping.ToProductViewModel(product);
            return View(ans);
        }

    }
}
