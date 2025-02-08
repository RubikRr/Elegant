using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Query.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class ProductController : Controller
{
    private readonly IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> _getProductByIdQuery;

    public ProductController(IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> getProductByIdQuery)
    {
        _getProductByIdQuery = getProductByIdQuery;
    }

    public async Task<IActionResult> GetProductById(Guid productId, CancellationToken cancellationToken = default)
    {
        var response =
            await _getProductByIdQuery.HandleAsync(new GetProductByIdRequest { ProductId = productId },
                cancellationToken);
        return View(nameof(GetProductById), Mapping.ToProductViewModel(response.Product));
    }
}