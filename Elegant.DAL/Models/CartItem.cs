namespace Elegant.DAL.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public Product Product { get; init; }
    public int Quantity { get; set; }

}