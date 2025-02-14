using Elegant.Business.Models.ViewModels.Product;

namespace Elegant.Business.Handlers.Product.Query.GetProductsByName;

public record GetProductsByNameResponse
{
    public required IReadOnlyCollection<ProductViewModel> Products { get; init; }
}