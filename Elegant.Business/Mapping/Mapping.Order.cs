using Elegant.Business.Models.ViewModels.Order;
using Elegant.Core.Models;

namespace Elegant.Business.Mapping;

public static partial class Mapping
{
    public static OrderViewModel ToOrderViewModel(Order order)
    {
        return new OrderViewModel
        {
            Id = order.Id,
            DeliveryInfo = ToUserDeliveryInfoViewModel(order.DeliveryInfo),
            Date = order.Date,
            Items = ToCartItemsViewModel(order.Items),
            Status = (OrderStatusViewModel)(order.Status),
        };
    }

    public static List<OrderViewModel> ToOrdersViewModel(List<Order> orders)
    {
        return orders.Select(ToOrderViewModel).ToList();
    }
}