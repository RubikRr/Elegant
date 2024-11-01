namespace Elegant.Web.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Cost { get; init; }
    public string Description { get; init; }
    public List<string> ImageItemsPaths { get; init; }
    public string ImagePath { get; init; }
}