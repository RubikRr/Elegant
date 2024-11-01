using System.ComponentModel.DataAnnotations;

namespace Elegant.Web.Areas.Admin.Models;

public class UpdateUserRoleViewModel
{
    public string UserId { get; init; }
    [Required(ErrorMessage = "Задайте права пользователя")]
    public string RoleName { get; init; }
}