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

    public async Task<IActionResult> Index()
    {
        var productsModel = await _productsStorage.GetAll();
        var test = Mapping.ToProductsViewModel(productsModel);
        return View(test);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string productName)
    {
        var productsModel = await _productsStorage.Search(productName);
        return View(Mapping.ToProductsViewModel(productsModel));
    }
}