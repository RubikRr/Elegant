using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.ViewModels;

public class UserDeliveryInfoViewModel
{
    [Required(ErrorMessage = "Впишите имя")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "ФИО содержит минимум 1 букву")]
    public string Name { get; init; }
    [Required(ErrorMessage = "Впишите адрес доставки")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Адрес содержит минимум 1 букву")]
    public string Address { get; init; }
    [Required(ErrorMessage = "Впишите номер телефона")]
    public string Phone { get; init; }
}