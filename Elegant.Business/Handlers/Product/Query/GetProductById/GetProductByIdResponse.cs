namespace Elegant.Business.Handlers.Product.Query.GetProductById;

public record GetProductByIdResponse
{
    public required Core.Models.Product? Product { get; init; }
}