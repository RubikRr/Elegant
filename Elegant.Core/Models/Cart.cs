
namespace Elegant.Core.Models;

public class Cart : IEntity
{
    public Guid Id { get; set; }
    public int UserId { get; init; }
    public List<CartOrder> Items { get; set; } = new();
}