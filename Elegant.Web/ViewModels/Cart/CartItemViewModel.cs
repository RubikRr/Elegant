using Elegant.Web.ViewModels.Product;

namespace Elegant.Web.ViewModels.Cart;

public class CartItemViewModel
{
    public Guid Id { get; } = Guid.Empty;
    public ProductViewModel Product { get; init; } = null!;
    public int Quantity { get; init; }
    public decimal Total => Product.Cost * Quantity;
}