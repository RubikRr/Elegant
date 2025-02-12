namespace Elegant.Web.ViewModels.Cart;

public class CartViewModel
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public List<CartItemViewModel> Items { get; init; } = new();

    public decimal Total
    {
        get { return Items.Sum(cartItem => cartItem.Total); }
    }

    public int Quantity
    {
        get { return Items.Sum(cartItem => cartItem.Quantity); }
    }
}