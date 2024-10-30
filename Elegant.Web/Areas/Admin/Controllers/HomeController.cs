using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Areas.Admin.Controllers
{
    [Area(OnlineShop.DB.Constants.AdminRoleName)]
    [Authorize(Roles = OnlineShop.DB.Constants.AdminRoleName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
