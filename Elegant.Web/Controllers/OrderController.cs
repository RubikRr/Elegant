using Elegant.Core.Models;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Elegant.Web.Helpers;
using Elegant.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrdersStorage _ordersStorage;

    private readonly ICartsStorage _cartsStorage;

    public OrderController(IOrdersStorage ordersStorage, ICartsStorage cartStorage)
    {
        _ordersStorage = ordersStorage;
        _cartsStorage = cartStorage;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Confirm(UserDeliveryInfoViewModel userDeliveryInfo)
    {
        if (ModelState.IsValid)
        {
            var cart = _cartsStorage.TryGetByUserId(Constants.UserId);
            var orderItems = new List<CartOrder>();
            orderItems.AddRange(cart.Items);
            var order = new Order
            {
                DeliveryInfo = Mapping.ToUserDeliveryInfoModel(userDeliveryInfo),
                Items = orderItems
            };
            _ordersStorage.Add(order);
            _cartsStorage.Clear(Constants.UserId);
            return View();
        }
        return RedirectToAction("Checkout", userDeliveryInfo);
    }

    public IActionResult Checkout()
    {
        return View();
    }
}