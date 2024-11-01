using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductsStorage _productsStorage;
    public ProductController(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }
    public IActionResult Index(Guid productId)
    {
        var product = _productsStorage.TryGetById(productId);
        //return product != null? product.ToString():$"Товар с индексом {id} не существует";
        var ans = Mapping.ToProductViewModel(product);
        return View(ans);
    }
}