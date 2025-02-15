using Elegant.Abstraction.Handlers.Query;

namespace Elegant.Business.Handlers.Product.Query.GetProductById;

public record GetProductByIdRequest : IQuery
{
    public Guid ProductId { get; init; }
}