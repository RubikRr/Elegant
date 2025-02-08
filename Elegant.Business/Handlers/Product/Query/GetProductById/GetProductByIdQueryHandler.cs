using Elegant.Abstraction.Handlers.Query;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Query.GetProductById;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdRequest,GetProductByIdResponse>
{
    private readonly IProductsStorage _productsStorage;

    public GetProductByIdQueryHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<GetProductByIdResponse> HandleAsync(GetProductByIdRequest query, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(new GetProductByIdResponse
        {
            Product = _productsStorage.GetById(query.ProductId)
        });
    }
}