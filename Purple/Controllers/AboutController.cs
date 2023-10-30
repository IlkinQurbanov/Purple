using Microsoft.AspNetCore.Mvc;

namespace Purple.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
