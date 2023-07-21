﻿using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;
using WomanShop.Helpers;
using WomanShop.Interfaces;
using WomanShop.Models;

namespace WomanShop.Controllers
{
    public class OrderController : Controller
    {
        private IOrdersStorage ordersStorage;

        private ICartsStorage cartsStorage;

        public OrderController(IOrdersStorage _ordersStorage, ICartsStorage _cartStorage)
        {
            ordersStorage = _ordersStorage;
            cartsStorage = _cartStorage;
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
                var cart = cartsStorage.TryGetByUserId(Constants.UserId);
                var orderItems = new List<CartItem>();
                orderItems.AddRange(cart.Items);
                var order = new Order
                {
                    DeliveryInfo = Mapping.ToUserDeliveryInfoModel(userDeliveryInfo),
                    Items = orderItems
                };
                ordersStorage.Add(order);
                //cartsStorage.Destroy(Constants.UserId);
                cartsStorage.Clear(Constants.UserId);
                return View();
            }
            return RedirectToAction("Checkout", userDeliveryInfo);
        }

        public IActionResult Checkout()
        {
            //var cart=cartsStorage.TryGetByUserId(Constants.UserId);
            return View();
        }
    }
}
