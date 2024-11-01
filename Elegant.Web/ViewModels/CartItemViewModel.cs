namespace Elegant.Web.ViewModels;

public class CartItemViewModel
{
    public Guid Id { get; }
    public ProductViewModel Product { get; init; }
    public int Quantity { get; init; }
    public decimal Total => Product.Cost * Quantity;
}