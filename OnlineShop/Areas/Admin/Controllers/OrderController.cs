using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;
using WomanShop.Helpers;
using WomanShop.Models;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class OrderController : Controller
    {
        private IOrdersStorage ordersStorage;
        public OrderController(IOrdersStorage _ordersStorage)
        {
            ordersStorage = _ordersStorage;
        }
        public IActionResult Index()
        {
            var orders = ordersStorage.GetAll();
            return View(Mapping.ToOrdersViewModel(orders));
        }
        public IActionResult Details(Guid orderId)
        {
            var order = ordersStorage.TryGetById(orderId);
            return View(Mapping.ToOrderViewModel(order));
        }
        [HttpPost]
        public IActionResult UpdateStatus(Guid orderId, OrderStatusViewModel status)
        {
            var newStatus = (OrderStatus)(status);
            ordersStorage.UpdateStatus(orderId, newStatus);
            return RedirectToAction("Index");
        }
    }
}
