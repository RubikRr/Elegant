namespace Elegant.DAL.Models;

public class Cart
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public List<CartItem> Items { get; set; } = new();
}