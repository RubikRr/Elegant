using System.ComponentModel.DataAnnotations;

namespace WomanShop.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare("Password",ErrorMessage ="Пароли не совпадат")]
        public string ConfirmPassword { get; set; }
    }
}
