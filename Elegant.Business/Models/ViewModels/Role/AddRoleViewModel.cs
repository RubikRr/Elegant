﻿using System.ComponentModel.DataAnnotations;

namespace Elegant.Business.Models.ViewModels.Role;

public class AddRoleViewModel
{
    [Required(ErrorMessage = "Введите название роли")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Минимальная длина 1.Максимальная 50")]
    public required string Name { get; init; }
}