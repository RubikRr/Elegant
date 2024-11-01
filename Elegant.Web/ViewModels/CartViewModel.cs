namespace Elegant.Web.ViewModels;

public class CartViewModel
{
    public Guid Id { get; init; }
    public int UserId { get; init; }
    public List<CartItemViewModel> Items { get; init; }

    public decimal Total
    {
        get
        {
            return Items?.Sum(cartItem => cartItem.Total) ?? 0;
        }
    }
    public int Quantity
    {
        get
        {
            return Items?.Sum(cartItem => cartItem.Quantity) ?? 0;
        }
    }
}