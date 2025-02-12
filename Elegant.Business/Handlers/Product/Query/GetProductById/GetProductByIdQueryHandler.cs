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
        var product = await _productsStorage.GetById(query.ProductId);

        if (product == null)
        {
            throw new Exception($"Товар с таким не был найден");
        }

        return new GetProductByIdResponse { Product = Mapping.ToProductViewModel(product) };
    }
}