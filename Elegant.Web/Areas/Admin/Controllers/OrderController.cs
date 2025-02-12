using Elegant.Business;
using Elegant.Business.Models.ViewModels.Order;
using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IOrdersStorage _ordersStorage;
    public OrderController(IOrdersStorage ordersStorage)
    {
        _ordersStorage = ordersStorage;
    }
    public IActionResult Index()
    {
        var orders = _ordersStorage.GetAll();
        return View(Mapping.ToOrdersViewModel(orders));
    }
    public IActionResult Details(Guid orderId)
    {
        var order = _ordersStorage.TryGetById(orderId);
        return View(Mapping.ToOrderViewModel(order));
    }
    [HttpPost]
    public IActionResult UpdateStatus(Guid orderId, OrderStatusViewModel status)
    {
        var newStatus = (OrderStatus)(status);
        _ordersStorage.UpdateStatus(orderId, newStatus);
        return RedirectToAction("Index");
    }
}