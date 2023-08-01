using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WomanShop.Areas.Admin.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите цену продукта")]
        [Range(0.0, 1000000, ErrorMessage = "Цена варьируется от 0 до 1000000")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Введите описание продукта")]
        [StringLength(10000, ErrorMessage = "Описание до 10000 символов")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Выберете фотографию")]
        public IFormFile UploadedImage { get; set; }
    }
}
