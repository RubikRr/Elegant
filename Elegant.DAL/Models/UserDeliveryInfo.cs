﻿using System.ComponentModel.DataAnnotations;

namespace WomanShop.Models
{
    public class UserDeliveryInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
