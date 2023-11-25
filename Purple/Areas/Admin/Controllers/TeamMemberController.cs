using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Helpers;
using Purple.Models;
using System.Net.Mime;

namespace Purple.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {

        public readonly AppDbContext appDbContext;
        public readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileService fileService;

        public TeamMemberController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            this.appDbContext = appDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await appDbContext.TeamMembers.ToListAsync();


            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(TeamMember teamMember)
        {

            if (!ModelState.IsValid) return View(teamMember);


            //Check is the file photo
            if (!fileService.IsImage(teamMember.Photo))
            {
                ModelState.AddModelError("Photo", "Seçdiyiniz fayl şəkil formatında deyil");
                return View(teamMember);
            }

            //Check the size of photo
            if (fileService.SizeCheck(teamMember.Photo))
            {
                ModelState.AddModelError("Photo", "Şəklin həcmi boyükdür");
                return View(teamMember);
            }

           



            teamMember.PhotoName = await  fileService.UploadAsync(webHostEnvironment.WebRootPath, teamMember.Photo);
            await appDbContext.AddAsync(teamMember);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var model = await appDbContext.TeamMembers.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);


        }

        [HttpPost]

        public async Task<IActionResult> DeleteComponent(int id)
        {

            var model = await appDbContext.TeamMembers.FindAsync(id);

            if (model == null) return NotFound();


            fileService.Delete(webHostEnvironment.WebRootPath, model.PhotoName);
            appDbContext.TeamMembers.Remove(model);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        



    }

}
