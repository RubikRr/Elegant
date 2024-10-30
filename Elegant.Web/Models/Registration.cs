using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Пароли не совпадат")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
