using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Elegant.Business.Models.ViewModels.Product;

public class CreateProductViewModel
{
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Введите цену продукта")]
    [Range(0.0, 1000000, ErrorMessage = "Цена варьируется от 0 до 1000000")]
    public decimal Cost { get; init; }

    [Required(ErrorMessage = "Введите описание продукта")]
    [StringLength(10000, ErrorMessage = "Описание до 10000 символов")]
    public string Description { get; init; } = string.Empty;

    public IFormFile[]? UploadedImage { get; init; }
}