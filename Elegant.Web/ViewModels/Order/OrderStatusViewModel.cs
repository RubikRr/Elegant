using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.ViewModels.Order;

public enum OrderStatusViewModel
{
    [Display(Name = "Новый")]
    New = 0,
    //TODO раскоментить когда займусь статусами
    // [Display(Name = "Подтвержденный")]
    // Confirmed = 1,
    // [Display(Name = "Оплаченный")]
    // Paid = 2,
    // [Display(Name = "Доставленный")]
    // Delivered = 3,
    // [Display(Name = "Завершенный")]
    // Complited = 4
}