using Elegant.Business.Services;
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

    public IActionResult Index()
    {
        var productsModel = _productsStorage.GetAll();
        var test = Mapping.ToProductsViewModel(productsModel);
        return View(test);
    }

    [HttpPost]
    public IActionResult Search(string productName)
    {
        var productsModel = _productsStorage.Search(productName);
        return View(Mapping.ToProductsViewModel(productsModel));
    }
}