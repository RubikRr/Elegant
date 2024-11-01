namespace Elegant.DAL.Models;

public class ImageItem
{
    public Guid Id { get; init; }
    public string ImagePath { get; init; }
    public Guid ProductId { get; init; }
        
    public Product Product { get; init; }
}