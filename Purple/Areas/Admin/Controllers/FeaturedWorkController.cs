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

        public FeaturedWorkController (AppDbContext appDbContext, 
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

            if(!ModelState.IsValid) return View(model);


            var featuredWork = new Purple.Models.FeaturedWork
            {
                Name = model.Name,
                Description = model.Description,
                Title = model.Title

            };
            bool hasError = false;
            foreach(var item in model.Photos)
            {
                if(!fileService.IsImage(item))
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


                        foreach(var item in model.Photos)
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




        }


    }

