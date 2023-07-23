using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public UserController(IUsersStorage _usersStorage,IRolesStorage _rolesStorage)
        {
            usersStorage = _usersStorage;
            rolesStorage = _rolesStorage;
            

        }

        public IActionResult Index()
        {
            var users = usersStorage.GetAll();
            return View(users);

        }

        public IActionResult Details(Guid userId)
        {
            var user = usersStorage.TryGetUserById(userId);
            return View(user);
        }

        public IActionResult Remove(Guid userId)
        {
            usersStorage.Remove(userId);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            ViewBag.Roles = new SelectList(rolesStorage.GetAll(), nameof(Models.Role.Name), nameof(Models.Role.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Add(UserViewModel user)
        {
            
            if (ModelState.IsValid)
            {
                usersStorage.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public IActionResult Update(Guid userId)
        {
            ViewBag.Roles = new SelectList(rolesStorage.GetAll(), nameof(Models.Role.Name), nameof(Models.Role.Name));
            var user=usersStorage.TryGetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                usersStorage.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public IActionResult UpdatePassword(Guid userId)
        {
            var user = usersStorage.TryGetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdatePassword(Guid userId,string password,string confirmPassword)
        {
            var user=usersStorage.TryGetUserById(userId);
            if (user == null)
            {
                ModelState.AddModelError("","Пользователь не наеден");
            }
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не должны совпадать");

            }
            if (ModelState.IsValid)
            {
                usersStorage.UpdatePassword(user,password);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult UpdateRole(Guid userId)
        {
            ViewBag.Roles = new SelectList(rolesStorage.GetAll(), nameof(Models.Role.Name), nameof(Models.Role.Name));
            var user = usersStorage.TryGetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateRole(Guid userId,string roleName)
        {
            var user = usersStorage.TryGetUserById(userId);
            var role = rolesStorage.TryGetByName(roleName);
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не наеден");
            }
            if (ModelState.IsValid)
            {
                usersStorage.UpdateRole(user, role);
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}
