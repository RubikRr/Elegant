using Elegant.Business;
using Elegant.Business.Mapping;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Elegant.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductsStorage _productsStorage;

    public HomeController(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var productsModel = await _productsStorage.GetAll(cancellationToken);
        var test = Mapping.ToProductsViewModel(productsModel);
        return View(test);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string productName, CancellationToken cancellationToken)
    {
        var productsModel = await _productsStorage.Search(productName, cancellationToken);
        return View(Mapping.ToProductsViewModel(productsModel));
    }
}