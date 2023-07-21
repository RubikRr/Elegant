using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    
    public class Role
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Введите название роли")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Минимальная длина 1.Максимальная 50")]
        public string Name { get; set; }

        public Role()
        {
            Id=Guid.NewGuid();
        }

        public Role(string name) : this()
        {
            Name = name;
        }
    }
}
