using Microsoft.AspNetCore.Mvc;

namespace Investing.Controllers
{
    public class UserRolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
