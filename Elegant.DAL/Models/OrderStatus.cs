using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public enum OrderStatus
    {
        New = 0,
        Confirmed = 1,
        Paid = 2,
        Delivered = 3,
        Complited = 4

    }
}
