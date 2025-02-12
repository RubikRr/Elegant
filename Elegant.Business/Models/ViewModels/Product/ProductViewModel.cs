namespace Elegant.Business.Models.ViewModels.Product;

public class ProductViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public string Description { get; init; } = string.Empty;
    public List<string> ImageItemsPaths { get; init; } = new();
    public string ImagePath { get; init; } = string.Empty;
}