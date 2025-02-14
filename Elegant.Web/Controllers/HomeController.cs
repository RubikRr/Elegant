using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Query.GetAllProducts;
using Elegant.Business.Handlers.Product.Query.GetProductsByName;
using Elegant.Business.Mapping;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class HomeController : Controller
{
    private readonly IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> _getAllProductsRequestHandler;
    private readonly IQueryHandler<GetProductsByNameRequest, GetProductsByNameResponse> _getProductsByNameRequestHandler;

    public HomeController(IQueryHandler<GetAllProductsRequest,
            GetAllProductsResponse> getAllProductsRequestHandler,
        IQueryHandler<GetProductsByNameRequest, GetProductsByNameResponse> getProductsByNameRequestHandler)
    {
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
        _getProductsByNameRequestHandler = getProductsByNameRequestHandler;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var response = await _getAllProductsRequestHandler.HandleAsync(new GetAllProductsRequest(), cancellationToken);
        return View(nameof(Index), response.Products);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string productName, CancellationToken cancellationToken)
    {
        var response = await _getProductsByNameRequestHandler.HandleAsync(new GetProductsByNameRequest { ProductName = productName },
            cancellationToken);
        return View(nameof(Search), response.Products);
    }
}