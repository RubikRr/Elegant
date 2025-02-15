using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetProductsByName;

public class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameRequest, GetProductsByNameResponse>
{
    private readonly IProductsStorage _productsStorage;

    public GetProductsByNameQueryHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<GetProductsByNameResponse> HandleAsync(GetProductsByNameRequest query,
        CancellationToken cancellationToken = default)
    {
        var product = await _productsStorage.GetByNameAsync(query.ProductName, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Товар с таким названием не был найден");
        }

        return new GetProductsByNameResponse { Products = Mapping.Mapping.ToProductsViewModel(product) };
    }
}