using WomanShop.Models;

namespace Elegant.Web.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get;}
        public ProductViewModel Product { get; set; }
        public int Quantity{ get; set; }
        public decimal Total { get { return Product.Cost * Quantity; } }
    }
}
