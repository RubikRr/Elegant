using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.ViewModels.User;

public class UserViewModel
{
    public string Id { get; init; } = string.Empty;
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]
    public string Email { get; init; } = string.Empty;
 
    [Required(ErrorMessage = "Введите ваше имя")]
    public string Name { get; init; } = string.Empty;
 
    [Required(ErrorMessage = "Впишите номер телефона")]
    public string Phone { get; init; } = string.Empty;
    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; set; } = string.Empty;

}