using System.ComponentModel.DataAnnotations;

namespace Elegant.Business.Models.ViewModels.User;

public class ResetPasswordInfo
{
    public string UserId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    [Required(ErrorMessage = "Необходимо подтвердить пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадат")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; init; } = string.Empty;
}