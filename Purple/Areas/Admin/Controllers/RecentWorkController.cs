using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Models;

namespace Purple.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecentWorkController : Controller
    {
        public readonly AppDbContext _appDbContext;

        public RecentWorkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _appDbContext.RecentWorks.ToListAsync();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]


        public async Task<IActionResult> Create(RecentWork recentWork)
        {
            if (!ModelState.IsValid) return View(recentWork);
         
            //Eyni title olub olmadiqin yoxlamaq
            bool isExist =await _appDbContext.RecentWorks.AnyAsync(rcw => rcw.Title == recentWork.Title);

            if(!isExist)
            {
                ModelState.AddModelError("Title", "We have this title");
                return View(recentWork);
            }

            await _appDbContext.RecentWorks.AddAsync(recentWork);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
