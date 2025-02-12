using Elegant.Abstraction.Handlers.Command;

namespace Elegant.Business.Handlers.Product.Command.RemoveProductById;

public record RemoveProductByIdRequest : ICommand
{
    public Guid ProductId { get; set; }
}