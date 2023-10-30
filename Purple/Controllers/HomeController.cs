using Microsoft.AspNetCore.Mvc;

namespace Purple.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
