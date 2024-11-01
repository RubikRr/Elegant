using Elegant.Abstraction.Entity;

namespace Elegant.Core.Models;

public class Order : IEntity
{
    public Guid Id { get; set; }
    public DeliveryInfo DeliveryInfo { get; init; } = new();
    public List<CartOrder> Items { get; init; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public DateTime Date { get; init; } = DateTime.Now;
}