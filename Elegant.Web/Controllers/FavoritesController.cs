using Elegant.Business.Mapping;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly IFavoritesStorage _favoritesStorage;

    public FavoritesController(IFavoritesStorage favoritesStorage, IProductRepository productRepository)
    {
        _favoritesStorage = favoritesStorage;
        _productRepository = productRepository;
    }
    public IActionResult Index()
    {
        var favoriteProducts = _favoritesStorage.GetAllProducts(DbConstants.UserId);
        return View(Mapping.ToProductsViewModel(favoriteProducts));
    }

    public async Task<IActionResult> Add(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
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