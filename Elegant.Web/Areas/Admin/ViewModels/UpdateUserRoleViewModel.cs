using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.ViewModels;

public class UpdateUserRoleViewModel
{
    public string UserId { get; init; }
    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; init; }
}