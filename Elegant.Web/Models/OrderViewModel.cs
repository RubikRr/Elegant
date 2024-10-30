using WomanShop.Models;

namespace Elegant.Web.Models
{

    public class OrderViewModel
    {

        public Guid Id { get; set; }
        public UserDeliveryInfoViewModel DeliveryInfo { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public decimal Total 
        { 
            get 
            {
                return Items.Sum(x => x.Total);
            }
        }

        public DateTime Date { get; set; }

        public OrderViewModel() { }
        public OrderViewModel(UserDeliveryInfoViewModel deliveryInfo,List<CartItemViewModel> items)
        {
            Id = Guid.NewGuid();
            Status = OrderStatusViewModel.New;
            DeliveryInfo= deliveryInfo;
            Items = items;
            Date = DateTime.Now;
        }

    }
}
