using System.ComponentModel.DataAnnotations;

namespace Elegant.Business.Models.ViewModels.User;

public class AddUserViewModel
{
    [Required(ErrorMessage = "Не указана почта")]
    [EmailAddress(ErrorMessage = "Введите валидный email")]
    public string Email { get; init; } = string.Empty;

    [Required(ErrorMessage = "Введите имя")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Введите номер телефона")]
    public string Phone { get; init; } = string.Empty;

    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Заполните поле с паролем")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Подтвердите пароль")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадат")]
    public string ConfirmPassword { get; init; } = string.Empty;
}