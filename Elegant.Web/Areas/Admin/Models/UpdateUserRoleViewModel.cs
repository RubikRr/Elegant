using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    public class UpdateUserRoleViewModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Задайте права пользователя")]
        public string RoleName { get; set; }
    }
}
