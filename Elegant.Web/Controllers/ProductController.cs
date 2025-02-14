using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Query.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class ProductController : Controller
{
    private readonly IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> _getProductByIdQueryHandler;

    public ProductController(IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> getProductByIdQueryHandler)
    {
        _getProductByIdQueryHandler = getProductByIdQueryHandler;
    }

    public async Task<IActionResult> GetProductById(Guid productId, CancellationToken cancellationToken = default)
    {
        var response =
            await _getProductByIdQueryHandler.HandleAsync(new GetProductByIdRequest { ProductId = productId },
                cancellationToken);
        return View(nameof(GetProductById), response.Product);
    }
}