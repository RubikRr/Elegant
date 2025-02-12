using Elegant.Business;
using Elegant.Business.Mapping;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class CartController : Controller
{
    private IProductsStorage ProductsStorage { get; }

    private ICartsStorage CartsStorage { get; set; }

    public CartController(IProductsStorage productsStorage, ICartsStorage cartStorage)
    {
        ProductsStorage = productsStorage;
        CartsStorage = cartStorage;
    }

    public async Task<IActionResult> Add(Guid productId,CancellationToken cancellationToken)
    {
        var product = await ProductsStorage.GetById(productId, cancellationToken);
        CartsStorage.Add(DbConstants.UserId, product);

        return RedirectToAction("Index");
    }


    public IActionResult Index()
    {
        var userCart = CartsStorage.TryGetByUserId(DbConstants.UserId);

        return View(Mapping.ToCartViewModel(userCart));
    }

    public IActionResult Clear()
    {
        CartsStorage.Clear(DbConstants.UserId);
        return RedirectToAction("Index");
    }
    public IActionResult ChangeCount(Guid cartId, Guid productId, string act)
    {
        CartsStorage.Change(cartId, productId, act);
        return RedirectToAction("Index");
    }
}