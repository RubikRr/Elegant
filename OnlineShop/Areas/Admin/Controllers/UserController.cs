using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using WomanShop.Areas.Admin.Models;
using WomanShop.Helpers;
using WomanShop.Interfaces;
using WomanShop.Models;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area(OnlineShop.DB.Constants.AdminRoleName)]
    [Authorize(Roles = OnlineShop.DB.Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private IUsersStorage usersStorage;
        private IRolesStorage rolesStorage;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(IUsersStorage _usersStorage, IRolesStorage _rolesStorage, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            usersStorage = _usersStorage;
            rolesStorage = _rolesStorage;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            return View(Mapping.ToUsersViewModel(users));
        }

        public async Task<IActionResult> Details(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = new List<string>(await userManager.GetRolesAsync(user));
                return View(Mapping.ToUserViewModel(user, userRoles));
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View("Details", userId);
                }
            }
            return RedirectToAction("Index");

        }

        //public IActionResult Add()
        //{
        //    ViewBag.Roles = new SelectList(rolesStorage.GetAll(), nameof(Models.Role.Name), nameof(Models.Role.Name));
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Add(UserViewModel user)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        usersStorage.Add(user);
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}
        public async Task<IActionResult> Update(string userId)
        {
            
            ViewBag.Roles = new SelectList(roleManager.Roles, nameof(IdentityRole.Name), nameof(IdentityRole.Name));
            var user =await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = new List<string>(await userManager.GetRolesAsync(user));
                return View(Mapping.ToUserViewModel(user, userRoles));
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel user)
        {
            var userUpdate = await userManager.FindByIdAsync(user.Id);

            if (user == null)
                ModelState.AddModelError("", "Пользователь не наеден");
            if (ModelState.IsValid)
            {
                userUpdate.Email = user.Email;
                userUpdate.PhoneNumber = user.Phone;
                userUpdate.UserName = user.Name;

                var result =await userManager.UpdateAsync(userUpdate);
                if (result.Succeeded)
                {
                    var userRoles = new List<string>(await userManager.GetRolesAsync(userUpdate));
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
            var user = await userManager.FindByIdAsync(resetPasswordInfo.UserId);
            if (user == null)
                ModelState.AddModelError("", "Пользователь не наеден");
           
            if (ModelState.IsValid)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, resetPasswordInfo.Password);

                if (result.Succeeded)
                {
                    var userRoles = new List<string>(await userManager.GetRolesAsync(user));
                    return View("Details", Mapping.ToUserViewModel(user,userRoles));
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

        //public IActionResult UpdateRole(Guid userId)
        //{
        //    ViewBag.Roles = new SelectList(rolesStorage.GetAll(), nameof(Models.Role.Name), nameof(Models.Role.Name));
        //    var user = usersStorage.TryGetUserById(userId);
        //    return View(user);
        //}
        //[HttpPost]
        //public IActionResult UpdateRole(Guid userId,string roleName)
        //{
        //    var user = usersStorage.TryGetUserById(userId);
        //    var role = rolesStorage.TryGetByName(roleName);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Пользователь не наеден");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        usersStorage.UpdateRole(user, role);
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

    }
}
