using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    public class AddUserViewModel
    {

        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Задайте права пользователя")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Заполните поле с паролем")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадат")]
        public string ConfirmPassword { get; set; }
    }
}
