using Elegant.Business.Models.ViewModels.Cart;

namespace Elegant.Business.Models.ViewModels.Order;

public class OrderViewModel
{
    public Guid Id { get; init; }
    public UserDeliveryInfoViewModel DeliveryInfo { get; init; } = null!;
    public List<CartItemViewModel> Items { get; init; } = null!;
    public OrderStatusViewModel Status { get; init; }
    public decimal Total
    {
        get
        {
            return Items.Sum(x => x.Total);
        }
    }

    public DateTime Date { get; init; }

    public OrderViewModel(UserDeliveryInfoViewModel deliveryInfo, List<CartItemViewModel> items)
    {
        Id = Guid.NewGuid();
        Status = OrderStatusViewModel.New;
        DeliveryInfo = deliveryInfo;
        Items = items;
        Date = DateTime.Now;
    }

    public OrderViewModel()
    {
    }
}