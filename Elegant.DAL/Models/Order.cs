namespace Elegant.DAL.Models;

public class Order
{
    public Guid Id { get; init; }
    public UserDeliveryInfo DeliveryInfo { get; init; }
    public List<CartItem> Items { get; init; }
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public DateTime Date { get; init; } = DateTime.Now;
}