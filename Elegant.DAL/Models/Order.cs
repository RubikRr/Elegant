namespace Elegant.DAL.Models
{
    
    public class Order
    {

        public Guid Id { get; set; }
        public UserDeliveryInfo DeliveryInfo { get; set; }
        public List<CartItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }

        public Order() 
        {
            Status = OrderStatus.New;
            Date = DateTime.Now;
        }
    }
}
