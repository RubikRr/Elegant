using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.ViewModels;

public class Login
{
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]

    public string Email { get; init; }
    [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
    public string Password { get; init; }
    public bool Remember { get; init; }
    public string ReturnUrl { get; init; }
}