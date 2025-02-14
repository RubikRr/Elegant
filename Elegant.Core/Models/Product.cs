namespace Elegant.Core.Models;

public class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<Image> ImageItems { get; set; } = new();
    public List<CartOrder> CartItems { get; init; } = new();
}