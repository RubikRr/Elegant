﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class FavoriteProductViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
    }
}
