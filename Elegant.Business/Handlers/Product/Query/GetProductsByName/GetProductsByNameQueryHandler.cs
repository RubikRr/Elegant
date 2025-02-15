using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetProductsByName;

public class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameRequest, GetProductsByNameResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductsByNameResponse> HandleAsync(GetProductsByNameRequest query,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByNameAsync(query.ProductName, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Товар с таким названием не был найден");
        }

        return new GetProductsByNameResponse { Products = Mapping.Mapping.ToProductsViewModel(product) };
    }
}