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
    private IProductRepository ProductRepository { get; }

    private ICartRepository CartRepository { get; set; }

    public CartController(IProductRepository productRepository, ICartRepository cartStorage)
    {
        ProductRepository = productRepository;
        CartRepository = cartStorage;
    }

    public async Task<IActionResult> Add(Guid productId,CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetByIdAsync(productId, cancellationToken);
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
        //CartRepository.Change(cartId, productId, act);
        return RedirectToAction("Index");
    }
}