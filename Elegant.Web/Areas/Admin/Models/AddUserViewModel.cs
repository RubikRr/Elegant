using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.Models
{
    public class AddUserViewModel
    {

        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; init; }
        [Required(ErrorMessage = "Задайте права пользователя")]
        public string RoleName { get; init; }
        [Required(ErrorMessage = "Заполните поле с паролем")]
        [DataType(DataType.Password)]
        public string Password { get; init; }
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадат")]
        public string ConfirmPassword { get; init; }
    }
}
