using Elegant.Business.Services;
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
        var product = _productsStorage.GetById(productId);
        var ans = Mapping.ToProductViewModel(product);
        return View();
    }
}