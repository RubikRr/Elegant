using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetAllProducts;

public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IProductsStorage _productsStorage;

    public GetAllProductsQueryHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<GetAllProductsResponse> HandleAsync(GetAllProductsRequest query,
        CancellationToken cancellationToken = default)
    {
        return new GetAllProductsResponse
            { Products = Mapping.Mapping.ToProductsViewModel(await _productsStorage.GetAll(cancellationToken)) };
    }
}