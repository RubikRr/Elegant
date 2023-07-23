using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WomanShop.Areas.Admin.Models;
using WomanShop.Interfaces;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area(OnlineShop.DB.Constants.AdminRoleName)]
    [Authorize(Roles = OnlineShop.DB.Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private IRolesStorage rolesStorage;
        public RoleController(IRolesStorage _rolesStorage)
        {
            rolesStorage = _rolesStorage;
        }

        public IActionResult Index()
        {
            var roles = rolesStorage.GetAll();
            return View(roles);
        }
        public IActionResult Remove(Guid roleId)
        {
            rolesStorage.Remove(roleId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Add(Role role)
        {
            if (rolesStorage.IsInStorage(role))
            {
                ModelState.AddModelError("", "Данная роль уже существуют.");
            }
            if (ModelState.IsValid)
            {
                rolesStorage.Add(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
