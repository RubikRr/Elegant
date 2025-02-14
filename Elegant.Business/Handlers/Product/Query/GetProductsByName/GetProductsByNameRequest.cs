using Elegant.Abstraction.Handlers.Query;

namespace Elegant.Business.Handlers.Product.Query.GetProductsByName;

public record GetProductsByNameRequest : IQuery
{
    public string ProductName { get; init; }
}