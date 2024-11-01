using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Models;

public class UserViewModel
{
    public string Id { get; init; }
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Введите ваше имя")]
    public string Name { get; init; }

    [Required(ErrorMessage = "Впишите номер телефона")]
    public string Phone { get; init; }
    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; set; }

}