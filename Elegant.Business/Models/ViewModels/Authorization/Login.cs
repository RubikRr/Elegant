using System.ComponentModel.DataAnnotations;

namespace Elegant.Business.Models.ViewModels.Authorization;

public class Login
{
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]

    public string Email { get; init; } = string.Empty;
    [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
    public string Password { get; init; } = string.Empty;
    public bool Remember { get; init; }
    public string ReturnUrl { get; init; } = string.Empty;
}