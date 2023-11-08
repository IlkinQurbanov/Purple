using Microsoft.AspNetCore.Mvc;

namespace Purple.Controllers
{
    public class RecentWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
