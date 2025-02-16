using Elegant.Business.Models.ViewModels.Order;
using Elegant.Business.Models.ViewModels.User;
using Elegant.Core.Models;

namespace Elegant.Business.Mapping;

public static partial class Mapping
{
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