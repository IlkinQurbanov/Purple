using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Models;
using System.Net.Mime;

namespace Purple.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {

        public readonly AppDbContext appDbContext;
        public readonly IWebHostEnvironment webHostEnvironment;

        public TeamMemberController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.appDbContext = appDbContext;
            this.webHostEnvironment = webHostEnvironment;
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
            if(!teamMember.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Seçdiyiniz fayl şəkil formatında deyil");
                return View(teamMember);
            }

            //Check the size of photo
            if (teamMember.Photo.Length/1024 > 300)
            {
                ModelState.AddModelError("Photo", "Şəklin həcmi boyükdür");
                return View(teamMember);
            }

            //This is for path the image file and make unique name for file 
            var fileName = $"{Guid.NewGuid()}_{teamMember.Photo.FileName}";
            var path = Path.Combine(webHostEnvironment.WebRootPath, "assets", "img", fileName);

                using (FileStream fileStream =  new FileStream(path,FileMode.Create,FileAccess.ReadWrite))
                {
                    await teamMember.Photo.CopyToAsync(fileStream);

                }
            teamMember.PhotoName = fileName;
            await appDbContext.AddAsync(teamMember);
            await appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    
            

    }
}
