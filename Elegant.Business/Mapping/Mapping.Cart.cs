using Elegant.Business.Models.ViewModels.Cart;
using Elegant.Business.Models.ViewModels.Order;
using Elegant.Business.Models.ViewModels.Product;
using Elegant.Business.Models.ViewModels.User;
using Elegant.Core.Models;

namespace Elegant.Business.Mapping;

public static partial class Mapping
{
    private static CartItemViewModel ToCartItemViewModel(CartOrder cartOrderModel)
    {
        return new CartItemViewModel
        {
            Product = ToProductViewModel(cartOrderModel.Product),
            Quantity = cartOrderModel.Quantity
        };
    }

    private static List<CartItemViewModel> ToCartItemsViewModel(List<CartOrder> cartItemsModel)
    {
        return cartItemsModel.Select(ToCartItemViewModel).ToList();
    }

    public static CartViewModel ToCartViewModel(Cart cartModel)
    {
        return new CartViewModel
        {
            Id = cartModel.Id,
            Items = ToCartItemsViewModel(cartModel.Items),
            UserId = cartModel.UserId
        };
    }
}