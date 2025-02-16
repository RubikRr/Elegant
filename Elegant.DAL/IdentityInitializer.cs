using Elegant.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Elegant.DAL;

public static class IdentityInitializer
{
    public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminEmail = "admin@gmail.com";
        var password = "_Aa123456";
        if (roleManager.FindByNameAsync(DbConstants.AdminRoleName).Result == null)
        {
            roleManager.CreateAsync(new IdentityRole(DbConstants.AdminRoleName)).Wait();
        }
        if (roleManager.FindByNameAsync(DbConstants.UserRoleName).Result == null)
        {
            roleManager.CreateAsync(new IdentityRole(DbConstants.UserRoleName)).Wait();
        }
        if (userManager.FindByEmailAsync(adminEmail).Result == null)
        {
            var admin = new User { Email = adminEmail, UserName = adminEmail };
            var result = userManager.CreateAsync(admin, password).Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(admin, DbConstants.AdminRoleName).Wait();
            }
        }
    }
}