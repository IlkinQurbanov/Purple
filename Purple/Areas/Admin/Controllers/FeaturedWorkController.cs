using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Helpers;
using Purple.Migrations;
using Purple.Models;
using Purple.ViewModels.FeaturedWorkComp;

namespace Purple.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeaturedWorkController : Controller
    {

        private readonly AppDbContext appDbContext;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FeaturedWorkController(AppDbContext appDbContext,
                                       IFileService fileService,
                                       IWebHostEnvironment webHostEnvironment)
        {
            this.appDbContext = appDbContext;
            this.fileService = fileService;
            this.webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var model = await appDbContext.FeaturedWork.FirstOrDefaultAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await appDbContext.FeaturedWork.FirstOrDefaultAsync();
            if (model != null) return NotFound();
            return View();
            ;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeaturedWorkCreateViewModel model)
        {
            var dBmodel = await appDbContext.FeaturedWork.FirstOrDefaultAsync();


            if (dBmodel != null) return BadRequest();

            if (!ModelState.IsValid) return View(model);


            var featuredWork = new Purple.Models.FeaturedWork
            {
                Name = model.Name,
                Description = model.Description,
                Title = model.Title

            };
            bool hasError = false;
            foreach (var item in model.Photos)
            {
                if (!fileService.IsImage(item))
                {
                    ModelState.AddModelError("Photos", $"{item.FileName} adlı fayl şəkil deyil");
                    hasError = true;
                    return View(model);
                }
                else
                {
                    if (!fileService.SizeCheck(item))
                    {
                        ModelState.AddModelError("Photos", $"{item.FileName} adlı faylın həcmi böyükdür");
                        hasError = true;
                        return View(model);

                    }

                }


            }

            if (hasError) return View(model);
            await appDbContext.FeaturedWork.AddAsync(featuredWork);
            await appDbContext.SaveChangesAsync();


            foreach (var item in model.Photos)
            {
                var featureWorkPhoto = new FeaturedWorkPhoto
                {

                    Name = await fileService.UploadAsync(webHostEnvironment.WebRootPath, item)

                };
                featureWorkPhoto.FeaturedWorkId = featuredWork.Id;
                await appDbContext.FeaturedWorkPhotos.AddAsync(featureWorkPhoto);
                await appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("index");

        }

        //Go to Update

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var dbmodel = await appDbContext.FeaturedWork.Include(pw => pw.FeaturedWorkPhotos)
                .OrderByDescending(fwp => fwp.Id)
                .FirstOrDefaultAsync();
            if (dbmodel == null) NotFound();


            var model = new FeaturedWorkUpdateViewModel
            {
                Title = dbmodel.Title,
                Name = dbmodel.Name,
                Description = dbmodel.Description,
                FeaturedWorkPhotos = dbmodel.FeaturedWorkPhotos.OrderByDescending(fwp => fwp.Id).ToList()
            };
            return View(model);
        }

        // Update

        public async Task<IActionResult> Update(FeaturedWorkUpdateViewModel model)
        {

            model.FeaturedWorkPhotos = await appDbContext.FeaturedWorkPhotos.ToListAsync();


            if (!ModelState.IsValid) return View(model);

            var dbmodel = await appDbContext.FeaturedWork.FirstOrDefaultAsync();
            if (dbmodel == null) return NotFound();


            dbmodel.Title = model.Title;
            dbmodel.Name = model.Name;
            dbmodel.Description = model.Description;

            if (model.Photos != null)
            {
                bool hasError = false;
                foreach (var item in model.Photos)
                {
                    if (fileService.IsImage(item))
                    {
                        ModelState.AddModelError("Photos", $"{item.FileName} Şəkil formatı deyil");
                        hasError = true;
                    }
                    else
                    {
                        if (fileService.SizeCheck(item))
                        {
                            ModelState.AddModelError("Photos", $"{item.FileName} Şəklin həcmi böyükdür");
                            hasError = true;
                        }

                    }
                }

                if (hasError)
                {
                    return View(model);
                }

                foreach (var item in model.Photos)
                {
                    var featuredPhoto = new FeaturedWorkPhoto();
                    featuredPhoto.Name = await fileService.UploadAsync(webHostEnvironment.WebRootPath, item);
                    featuredPhoto.FeaturedWorkId = dbmodel.Id;
                    await appDbContext.FeaturedWorkPhotos.AddAsync(featuredPhoto);
                    await appDbContext.SaveChangesAsync();
                }
            }

            await appDbContext.SaveChangesAsync();
            return RedirectToAction("index");


        }

        //This method is for delete single photo in Featured Work
        [HttpGet]

        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await appDbContext.FeaturedWorkPhotos.FindAsync(id);
            if (photo == null) return NotFound();
            fileService.Delete(webHostEnvironment.WebRootPath, photo.Name);
            appDbContext.FeaturedWorkPhotos.Remove(photo);
            await appDbContext.SaveChangesAsync();

            return Json(new { success = true, redirectTo = Url.Action(nameof(Update))});

           }


    }
}

