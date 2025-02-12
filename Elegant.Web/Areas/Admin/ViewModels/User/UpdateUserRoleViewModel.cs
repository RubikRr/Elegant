using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.ViewModels.User;

public class UpdateUserRoleViewModel
{
    public string UserId { get; init; } = string.Empty;

    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; init; } = string.Empty;
}