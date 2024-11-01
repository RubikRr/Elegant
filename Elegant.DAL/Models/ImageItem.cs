namespace Elegant.DAL.Models;

public class ImageItem
{
    public Guid Id { get; init; }
    public string ImagePath { get; init; } = string.Empty;
    public Guid ProductId { get; init; }

    public required Product Product { get; init; } = new();
}