using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using WomanShop.Interfaces;
using WomanShop.Models;

namespace WomanShop.Controllers
{
    public class AuthController : Controller
    {
        private IUsersStorage usersStorage;
        public AuthController(IUsersStorage _usersStorage)
        {
            usersStorage = _usersStorage;
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
           
            if (usersStorage.TryGetUserByEmail(login.Email) == null)
            {
                ModelState.AddModelError("","Пользователь с таким email не найден");
            }
            if (!usersStorage.IsCorrectPassword(login))
            {
                ModelState.AddModelError("", "Пароли не совпадают");
            }
            if (ModelState.IsValid)
                return RedirectToAction("index", "home");
            return View(login);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            if (usersStorage.TryGetUserByEmail(registration.Email) != null)
            {
                ModelState.AddModelError("", "Пользователь с данным логином уже существует");
            }
            if (registration.Password==registration.Email)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                var user = new User(registration.Email, registration.Password);
                usersStorage.Add(user);
                return RedirectToAction("index", "home");
            }
            return View(registration);
        }
        public IActionResult Registration()
        {
            return View();
        }

    }
}
