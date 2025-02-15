using Elegant.Abstraction.Handlers.Command;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Command.RemoveProductById;

public class RemoveProductByIdCommandHandler : ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse>
{
    private readonly IProductRepository _productRepository;

    public RemoveProductByIdCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<RemoveProductByIdResponse> HandleAsync(RemoveProductByIdRequest command,
        CancellationToken cancellationToken = default)
    {
        await _productRepository.RemoveAsync(command.ProductId, cancellationToken);
        return new RemoveProductByIdResponse();
    }
}