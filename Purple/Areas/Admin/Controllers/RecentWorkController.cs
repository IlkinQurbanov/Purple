using Microsoft.AspNetCore.Mvc;

namespace Purple.Areas.Admin.Controllers
{
    public class RecentWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
