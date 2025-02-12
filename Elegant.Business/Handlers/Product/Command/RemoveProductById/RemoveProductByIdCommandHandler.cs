using Elegant.Abstraction.Handlers.Command;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Command.RemoveProductById;

public class RemoveProductByIdCommandHandler : ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse>
{
    private readonly IProductsStorage _productsStorage;

    public RemoveProductByIdCommandHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<RemoveProductByIdResponse> HandleAsync(RemoveProductByIdRequest command,
        CancellationToken cancellationToken = default)
    {
        await _productsStorage.Remove(command.ProductId, cancellationToken);
        return new RemoveProductByIdResponse();
    }
}