using Elegant.Abstraction.Handlers.Command;
using Elegant.Business.Models.ViewModels.Product;

namespace Elegant.Business.Handlers.Product.Command.AddProduct;

public record AddProductRequest : ICommand
{
    public CreateProductViewModel ViewModel { get; init; } = new();
}