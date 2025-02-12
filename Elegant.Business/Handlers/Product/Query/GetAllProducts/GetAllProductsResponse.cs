using Elegant.Business.Models.ViewModels.Product;

namespace Elegant.Business.Handlers.Product.Query.GetAllProducts;

public sealed record GetAllProductsResponse()
{
    public required IReadOnlyCollection<ProductViewModel> Products { get; init; }
}
