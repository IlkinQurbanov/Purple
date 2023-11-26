using Microsoft.AspNetCore.Mvc;

namespace Purple.Controllers
{
    public class ContactController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
