using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    public class ResetPasswordInfo
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадат")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
