using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WomanShop.Areas.Admin.Models;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area(OnlineShop.DB.Constants.AdminRoleName)]
    [Authorize(Roles = OnlineShop.DB.Constants.AdminRoleName)]
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController( RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Remove(Guid roleId)
        {
            var role= roleManager.FindByIdAsync(roleId.ToString()).Result;
            if (role != null)
            {
                roleManager.DeleteAsync(role); 
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Add(AddRoleViewModel role)
        {

            if (roleManager.FindByNameAsync(role.Name).Result != null)
            {
                ModelState.AddModelError("", "Данная роль уже существуют.");
            }
            
            
           
            if (ModelState.IsValid)
            {
                roleManager.CreateAsync(new IdentityRole(role.Name)).Wait();
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
