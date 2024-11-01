using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Models;

public class Registration
{
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
    [DataType(DataType.Password)]
    public string Password { get; init; }
    [Required(ErrorMessage = "Необходимо подтвердить пароль")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадат")]
    public string ConfirmPassword { get; init; }

    public string ReturnUrl { get; init; }
}