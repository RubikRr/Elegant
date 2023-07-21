using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    public class UpdatePasswordInfo
    {
        [Required(ErrorMessage = "Не указана почта")]
        public Guid UserId {get;set;}

        [Required(ErrorMessage = "Необходимо заполнить поле с паролем")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадат")]
        public string ConfirmPassword { get; set; }
    }
}
