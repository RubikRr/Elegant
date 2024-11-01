
namespace Elegant.DAL.Models;

public class Product
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; } 
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public List<ImageItem> ImageItems { get; init; } = new();
    public List<CartItem> CartItems { get; init; } = new();

  
}