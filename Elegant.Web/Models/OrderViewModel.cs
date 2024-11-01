namespace Elegant.Web.Models;

public class OrderViewModel
{
    public Guid Id { get; init; }
    public UserDeliveryInfoViewModel DeliveryInfo { get; init; }
    public List<CartItemViewModel> Items { get; init; }
    public OrderStatusViewModel Status { get; init; }
    public decimal Total
    {
        get
        {
            return Items.Sum(x => x.Total);
        }
    }

    public DateTime Date { get; init; }

    public OrderViewModel() { }
    public OrderViewModel(UserDeliveryInfoViewModel deliveryInfo, List<CartItemViewModel> items)
    {
        Id = Guid.NewGuid();
        Status = OrderStatusViewModel.New;
        DeliveryInfo = deliveryInfo;
        Items = items;
        Date = DateTime.Now;
    }

}