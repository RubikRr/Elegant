namespace Elegant.Web.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }

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
}
