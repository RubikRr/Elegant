using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
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
        var favoriteProducts = _favoritesStorage.GetAllProducts(Constants.UserId);
        return View(Mapping.ToProductsViewModel(favoriteProducts));
    }

    public IActionResult Add(Guid productId)
    {
        var product = _productsStorage.TryGetById(productId);
        if (product == null) { return RedirectToAction("Index"); }
        _favoritesStorage.Add(Constants.UserId, product);
        return RedirectToAction("Index");
    }

    public IActionResult Remove(Guid productId)
    {
        _favoritesStorage.Remove(Constants.UserId, productId);
        return RedirectToAction("Index");
    }
}