using System.Security.Claims;
using System.Security.Principal;
using Elegant.Business.Mapping;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

[Authorize]
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
        var product = await ProductsStorage.GetByIdAsync(productId, cancellationToken);
        //CartsStorage.Add(DbConstants.UserId, product);

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //var userCart = CartsStorage.TryGetByUserId(DbConstants.UserId);

        return View();
    }

    public IActionResult Clear()
    {
        //CartsStorage.Clear(DbConstants.UserId);
        return RedirectToAction("Index");
    }
    public IActionResult ChangeCount(Guid cartId, Guid productId, string act)
    {
        CartsStorage.Change(cartId, productId, act);
        return RedirectToAction("Index");
    }
}