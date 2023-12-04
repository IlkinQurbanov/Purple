using Microsoft.AspNetCore.Mvc;

namespace Purple.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
