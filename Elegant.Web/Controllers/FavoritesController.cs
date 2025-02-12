using Elegant.Business.Mapping;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly IProductsStorage _productsStorage;
    private readonly IFavoritesStorage _favoritesStorage;

    public FavoritesController(IFavoritesStorage favoritesStorage, IProductsStorage productsStorage)
    {
        _favoritesStorage = favoritesStorage;
        _productsStorage = productsStorage;
    }
    public IActionResult Index()
    {
        var favoriteProducts = _favoritesStorage.GetAllProducts(DbConstants.UserId);
        return View(Mapping.ToProductsViewModel(favoriteProducts));
    }

    public async Task<IActionResult> Add(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productsStorage.GetById(productId, cancellationToken);
        if (product == null) { return RedirectToAction("Index"); }
        _favoritesStorage.Add(DbConstants.UserId, product);
        return RedirectToAction("Index");
    }

    public IActionResult Remove(Guid productId)
    {
        _favoritesStorage.Remove(DbConstants.UserId, productId);
        return RedirectToAction("Index");
    }
}