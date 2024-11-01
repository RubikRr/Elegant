namespace Elegant.Core.Models;

public class CartOrder : IEntity
{
    public Guid Id { get; set; }
    public Product Product { get; init; } = new();
    public int Quantity { get; set; }

}