using Elegant.DAL;
using Elegant.Web.Areas.Admin.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Areas.Admin.Controllers;

[Area(DbConstants.AdminRoleName)]
[Authorize(Roles = DbConstants.AdminRoleName)]
public class RoleController : Controller
{

    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }
    public IActionResult Remove(Guid roleId)
    {
        var role = _roleManager.FindByIdAsync(roleId.ToString()).Result;
        if (role != null)
        {
            _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Add(AddRoleViewModel role)
    {

        if (_roleManager.FindByNameAsync(role.Name).Result != null)
        {
            ModelState.AddModelError("", "Данная роль уже существуют.");
        }

        if (ModelState.IsValid)
        {
            _roleManager.CreateAsync(new IdentityRole(role.Name)).Wait();
            return RedirectToAction("Index");
        }
        return View(role);
    }
    public IActionResult Add()
    {
        return View();
    }
}