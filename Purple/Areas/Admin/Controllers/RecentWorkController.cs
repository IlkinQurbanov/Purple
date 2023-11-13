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

            if(isExist)
            {
                ModelState.AddModelError("Title", "We have this title");
                return View(recentWork);
            }

            await _appDbContext.RecentWorks.AddAsync(recentWork);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        //Update

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var dbRcomp = await _appDbContext.RecentWorks.FindAsync(id);
            if (dbRcomp == null) return NotFound();

            return View(dbRcomp);

        }

        [HttpPost]

            public async Task<IActionResult> Update(int id,RecentWork recentWork)
        {
            if (!ModelState.IsValid) return View(recentWork);

            if (id != recentWork.Id) return BadRequest();

            var dbRecentWork = await _appDbContext.RecentWorks.FindAsync(id);

            if (dbRecentWork == null) return NotFound();

            bool isExist = await _appDbContext.RecentWorks
            .AnyAsync(rw => rw.Title.ToLower().Trim() == recentWork.Title.ToLower().Trim() && rw.Id != recentWork.Id);

            if (isExist)
            {
                ModelState.AddModelError("Title", "We have this title");
                return View(recentWork);

            }

            dbRecentWork.Title = recentWork.Title;
            dbRecentWork.Desc = recentWork.Desc;
            dbRecentWork.ImagePath = recentWork.ImagePath;

            await _appDbContext.SaveChangesAsync();


            return RedirectToAction("Index");

        }


        //Delete function

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var dbRcomp = await _appDbContext.RecentWorks.FindAsync();
            if (dbRcomp == null) return NotFound();
            return View(dbRcomp);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var dbRcomp = await _appDbContext.RecentWorks.FindAsync(id);
            if (dbRcomp == null) return NotFound();
            _appDbContext.RecentWorks.Remove(dbRcomp);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


    }
}
