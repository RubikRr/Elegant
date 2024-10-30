using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{

    public class AddRoleViewModel
    {

        [Required(ErrorMessage = "Введите название роли")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Минимальная длина 1.Максимальная 50")]
        public string Name { get; set; }
    }
}
