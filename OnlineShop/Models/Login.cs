using System.ComponentModel.DataAnnotations;

namespace WomanShop.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }
}
