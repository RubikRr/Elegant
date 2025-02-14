using Elegant.Abstraction.Handlers.Command;
using Elegant.Business.Models.ViewModels.Product;

namespace Elegant.Business.Handlers.Product.Command.UpdateProduct;

public record UpdateProductRequest : ICommand
{
    public UpdateProductViewModel ViewModel { get; init; } = new();
    
    public string ProductImageDirectoryPath { get; init; } = string.Empty;
}