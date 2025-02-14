using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Query.GetAllProducts;
using Elegant.Business.Mapping;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Elegant.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductsStorage _productsStorage;
    private readonly IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> _getAllProductsRequestHandler;

    public HomeController(IProductsStorage productsStorage, IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> getAllProductsRequestHandler)
    {
        _productsStorage = productsStorage;
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = await _getAllProductsRequestHandler.HandleAsync(new GetAllProductsRequest(), cancellationToken);
        return View(nameof(Index), response.Products);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string productName, CancellationToken cancellationToken)
    {
        var productsModel = await _productsStorage.Search(productName, cancellationToken);
        return View(Mapping.ToProductsViewModel(productsModel));
    }
}