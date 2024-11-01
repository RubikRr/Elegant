using Elegant.DAL;
using Elegant.DAL.Models;
using Elegant.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpPost]
    public IActionResult Login(Login login)
    {
        var user = _userManager.FindByEmailAsync(login.Email).Result;
        if (user != null)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.Remember, false).Result;
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль");
                }
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
        _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    [HttpPost]
    public IActionResult Registration(Registration registration)
    {
        //if (usersStorage.TryGetUserByEmail(registration.Email) != null)
        //{
        //    ModelState.AddModelError("", "Пользователь с данным логином уже существует");
        //}
        if (registration.Password == registration.Email)
        {
            ModelState.AddModelError("", "Логин и пароль не должны совпадать");
        }
        if (ModelState.IsValid)
        {
            //var user = new UserViewModel(registration.Email, registration.Password);
            var user = new User { UserName = registration.Email, Email = registration.Email };
            var result = _userManager.CreateAsync(user, registration.Password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                var loginResult = _signInManager.PasswordSignInAsync(registration.Email, registration.Password, false, false).Result;
                if (loginResult.Succeeded)
                    return Redirect(registration.ReturnUrl);
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
    public IActionResult Registration(string returnUrl)
    {
        return View(new Registration() { ReturnUrl = returnUrl });
    }

}