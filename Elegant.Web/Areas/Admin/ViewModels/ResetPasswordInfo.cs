using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.ViewModels;

public class ResetPasswordInfo
{
    public string UserId { get; init; }

    [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    [Required(ErrorMessage = "Необходимо подтвердить пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадат")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; init; }
}