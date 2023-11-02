using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;

namespace Purple.Controllers
{
    public class WorkController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public WorkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public  async Task<IActionResult> Index()
        {

            var model = await _appDbContext.Categories.Include(x => x.Components).ToListAsync();

            return View(model);
        }
    }
}
