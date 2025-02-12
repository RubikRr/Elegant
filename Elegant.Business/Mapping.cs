using Elegant.Business.Models.ViewModels.Cart;
using Elegant.Business.Models.ViewModels.Order;
using Elegant.Business.Models.ViewModels.Product;
using Elegant.Business.Models.ViewModels.User;
using Elegant.Core.Models;

namespace Elegant.Business;

public static class Mapping
{
    public static ProductViewModel ToProductViewModel(Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
            ImageItemsPaths = product.ImageItems.Select(imageItem => imageItem.ImagePath).ToList(),
            ImagePath = product.ImagePath
        };
    }
    public static Product ToProductModel(ProductViewModel productViewModel)
    {
        return new Product
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            Cost = productViewModel.Cost,
            Description = productViewModel.Description,
            ImagePath = productViewModel.ImagePath
        };
    }
    public static IReadOnlyCollection<ProductViewModel> ToProductsViewModel(List<Product> productsModel)
    {
        return productsModel.Select(ToProductViewModel).ToList();
    }

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

    public static DeliveryInfo ToUserDeliveryInfoModel(UserDeliveryInfoViewModel userInfo)
    {
        return new DeliveryInfo
        {
            Name = userInfo.Name,
            Address = userInfo.Address,
            Phone = userInfo.Phone
        };
    }

    private static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(DeliveryInfo info)
    {
        return new UserDeliveryInfoViewModel
        {
            Name = info.Name,
            Address = info.Address,
            Phone = info.Phone
        };
    }
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

    private static UserViewModel ToUserViewModel(User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Name = user.UserName ?? string.Empty,
            Phone = user.PhoneNumber ?? string.Empty,
        };
    }
    public static UserViewModel ToUserViewModel(User user, List<string> roles)
    {
        var newUser = ToUserViewModel(user);
        newUser.RoleName = string.Join(" ", roles);
        return newUser;
    }
    public static List<UserViewModel> ToUsersViewModel(List<User> users)
    {
        return users.Select(ToUserViewModel).ToList();
    }
}