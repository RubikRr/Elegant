using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetAllProducts;

public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllProductsResponse> HandleAsync(GetAllProductsRequest query,
        CancellationToken cancellationToken = default)
    {
        return new GetAllProductsResponse
            { Products = Mapping.Mapping.ToProductsViewModel(await _productRepository.GetAllAsync(cancellationToken)) };
    }
}