using Microsoft.AspNetCore.Mvc;
using WomanShop.Interfaces;
using WomanShop.Models;
using Microsoft.AspNetCore.Identity;
using OnlineShop.DB.Models;
using Serilog.Core;
using OnlineShop.DB;

namespace WomanShop.Controllers
{
    public class AuthController : Controller
    {
        private IUsersStorage usersStorage;

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(IUsersStorage _usersStorage, SignInManager<User> signInManager,UserManager<User> userManager)
        {
            usersStorage = _usersStorage;
            this.signInManager = signInManager;
            this.userManager = userManager;

        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
           
            //if (usersStorage.TryGetUserByEmail(login.Email) == null)
            //{
            //    ModelState.AddModelError("","Пользователь с таким email не найден");
            //}
            //if (!usersStorage.IsCorrectPassword(login))
            //{
            //    ModelState.AddModelError("", "Пароли не совпадают");
            //}
            if (ModelState.IsValid) 
            {
                var result = signInManager.PasswordSignInAsync(login.Email, login.Password, login.Remember, false).Result;
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }
                else 
                {
                    ModelState.AddModelError("", "Неправильный пароль");
                }
            }
            return View(login);
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new Login() { ReturnUrl = returnUrl });
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            //if (usersStorage.TryGetUserByEmail(registration.Email) != null)
            //{
            //    ModelState.AddModelError("", "Пользователь с данным логином уже существует");
            //}
            if (registration.Password==registration.Email)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                //var user = new UserViewModel(registration.Email, registration.Password);
                var user = new User { UserName = registration.Email, Email = registration.Email };
                var result = userManager.CreateAsync(user, registration.Password).Result;
                if (result.Succeeded) 
                {
                    userManager.AddToRoleAsync(user, OnlineShop.DB.Constants.UserRoleName).Wait();
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(registration);
                }
            }
            return View(registration);
        }
        public IActionResult Registration()
        {
            return View();
        }

    }
}
