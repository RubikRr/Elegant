using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class CartController : Controller
{
    //1
    ////2
    /////
    private IProductsStorage ProductsStorage { get; }

    private ICartsStorage CartsStorage { get; set; }

    public CartController(IProductsStorage productsStorage, ICartsStorage cartStorage)
    {
        ProductsStorage = productsStorage;
        CartsStorage = cartStorage;
    }

    public IActionResult Add(Guid productId)
    {
        var product = ProductsStorage.TryGetById(productId);
        CartsStorage.Add(Constants.UserId, product);

        return RedirectToAction("Index");
    }


    public IActionResult Index()
    {
        var userCart = CartsStorage.TryGetByUserId(Constants.UserId);

        return View(Mapping.ToCartViewModel(userCart));
    }

    public IActionResult Clear()
    {
        CartsStorage.Clear(Constants.UserId);
        return RedirectToAction("Index");
    }
    public IActionResult ChangeCount(Guid cartId, Guid productId, string act)
    {
        CartsStorage.Change(cartId, productId, act);
        return RedirectToAction("Index");
    }
}