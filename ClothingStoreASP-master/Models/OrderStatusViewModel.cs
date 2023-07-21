using System.ComponentModel.DataAnnotations;

namespace WomanShop.Models
{
    public enum OrderStatusViewModel
    {
        [Display(Name = "Новый")]
        New = 0,
        [Display(Name = "Подтвержденный")]
        Confirmed = 1,
        [Display(Name = "Оплаченный")]
        Paid = 2,
        [Display(Name = "Доставленный")]
        Delivered = 3,
        [Display(Name = "Завершенный")]
        Complited = 4

    }
}
