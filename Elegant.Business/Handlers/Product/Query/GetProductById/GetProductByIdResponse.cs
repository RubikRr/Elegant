using Elegant.Business.Models.ViewModels.Product;

namespace Elegant.Business.Handlers.Product.Query.GetProductById;

public record GetProductByIdResponse
{
    public required ProductViewModel Product { get; init; }
}