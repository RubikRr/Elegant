using Elegant.Business;
using Elegant.Business.Models.ViewModels.User;
using Elegant.Core.Models;
using Elegant.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Elegant.Web.Areas.Admin.Controllers;

[Area(DbConstants.AdminRoleName)]
[Authorize(Roles = DbConstants.AdminRoleName)]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(Mapping.ToUsersViewModel(users));
    }

    public async Task<IActionResult> Details(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = new List<string>(await _userManager.GetRolesAsync(user));
            return View(Mapping.ToUserViewModel(user, userRoles));
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View("Details");
            }
        }

        return RedirectToAction("Index");
    }

    public IActionResult Add()
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
        return View();
    }

    [HttpPost]
    public IActionResult Add(AddUserViewModel user)
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
        if (ModelState.IsValid)
        {
            var newUser = new User { UserName = user.Name, PhoneNumber = user.Phone, Email = user.Email };
            var result = _userManager.CreateAsync(newUser, user.Password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(newUser, user.RoleName).Wait();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(user);
            }

            return RedirectToAction("Index");
        }

        return View(user);
    }

    public async Task<IActionResult> Update(string userId)
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = new List<string>(await _userManager.GetRolesAsync(user));
            return View(Mapping.ToUserViewModel(user, userRoles));
        }

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserViewModel user)
    {
        var userUpdate = await _userManager.FindByIdAsync(user.Id);

        if (ModelState.IsValid)
        {
            userUpdate.Email = user.Email;
            userUpdate.PhoneNumber = user.Phone;
            userUpdate.UserName = user.Name;

            var result = await _userManager.UpdateAsync(userUpdate);
            if (result.Succeeded)
            {
                var userRoles = new List<string>(await _userManager.GetRolesAsync(userUpdate));
                return View("Details", Mapping.ToUserViewModel(userUpdate, userRoles));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(user);
            }
        }

        return View(user);
    }

    public IActionResult ResetPassword(string userId)
    {
        return View(new ResetPasswordInfo { UserId = userId });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordInfo resetPasswordInfo)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordInfo.UserId);
        if (user == null)
            ModelState.AddModelError("", "Пользователь не наеден");

        if (ModelState.IsValid)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordInfo.Password);

            if (result.Succeeded)
            {
                var userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                return View("Details", Mapping.ToUserViewModel(user, userRoles));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(resetPasswordInfo);
            }
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateRoleAsync(string userId)
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
        var user = _userManager.FindByIdAsync(userId).Result;
        var userRoles = new List<string>(await _userManager.GetRolesAsync(user));
        if (userRoles.Count == 0)
        {
            userRoles = [DbConstants.UserRoleName];
            _userManager.AddToRoleAsync(user, DbConstants.UserRoleName).Wait();
        }

        return View(new UpdateUserRoleViewModel { RoleName = userRoles.First(), UserId = user.Id.ToString() });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRoleAsync(UpdateUserRoleViewModel updateUserRole)
    {
        var user = _userManager.FindByIdAsync(updateUserRole.UserId).Result;

        if (ModelState.IsValid)
        {
            var oldRole = new List<string>(await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (oldRole != updateUserRole.RoleName)
            {
                if (!oldRole.IsNullOrEmpty())
                {
                    _userManager.RemoveFromRoleAsync(user, oldRole).Wait();
                    _userManager.AddToRoleAsync(user, updateUserRole.RoleName).Wait();
                }
                else
                {
                    _userManager.AddToRoleAsync(user, DbConstants.UserRoleName).Wait();
                }
            }


            return RedirectToAction("Index");
        }
        return View();
    }
}