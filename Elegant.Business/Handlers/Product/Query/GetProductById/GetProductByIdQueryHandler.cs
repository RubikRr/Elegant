using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetProductById;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IProductsStorage _productsStorage;

    public GetProductByIdQueryHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<GetProductByIdResponse> HandleAsync(GetProductByIdRequest query,
        CancellationToken cancellationToken = default)
    {
        var product = await _productsStorage.GetByIdAsync(query.ProductId, cancellationToken);

        if (product == null)
        {
            throw new Exception($"Товар с таким id не был найден");
        }

        return new GetProductByIdResponse { Product = Mapping.Mapping.ToProductViewModel(product) };
    }
}