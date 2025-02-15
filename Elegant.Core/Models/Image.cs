namespace Elegant.Core.Models;

public class Image : IEntity
{
    public Guid Id { get; set; }
    public string ImagePath { get; init; } = string.Empty;
    public Guid ProductId { get; init; }
    public Product Product { get; init; } = new();
}