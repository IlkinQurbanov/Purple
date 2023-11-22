using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;

namespace Purple.Controllers
{
    public class RecentWorkController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public RecentWorkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public async Task<IActionResult> Index()
        {

            var recentWorkComponentss = await _appDbContext.RecentWorks
                .OrderByDescending(rcw => rcw.Id).Take(3).ToListAsync();

            return View(recentWorkComponentss);
        }
    }
}
