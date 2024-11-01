using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.ViewModels;

public class EditProductViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    [Required(ErrorMessage = "Введите цену продукта")]
    [Range(0.0, 1000000, ErrorMessage = "Цена варьируется от 0 до 1000000")]
    public decimal Cost { get; init; }
    [Required(ErrorMessage = "Введите описание продукта")]
    [StringLength(10000, ErrorMessage = "Описание до 10000 символов")]
    public string Description { get; init; }
    [Required(ErrorMessage = "Выберете фотографию")]
    public string ImagePath { get; set; }
    public IFormFile UploadedImage { get; init; }
}