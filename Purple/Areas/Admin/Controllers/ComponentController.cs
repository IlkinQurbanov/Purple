using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Models;
using Purple.ViewModels.Work;

namespace Purple.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComponentController : Controller
    {
        private readonly AppDbContext _dbContext;


        public ComponentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult>  Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var selectedList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                selectedList.Add(new SelectListItem
                {
                    Text = category.Title,
                    Value = category.Id.ToString()
                });
            }

            var model = new WorkCreateViewModel
            { 
            
            Items = selectedList
            
            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(WorkCreateViewModel component)
        {


            var component1 = new Models.Component();
            component1.Title = component.Title;
            component1.Desc = component.Desc;
            component1.ImagePath = component.ImagePath;

            foreach(var item in component.CategoryIds)
            {
                if(await _dbContext.Categories.FindAsync(item) != null)
                {
                    component1.CategoryComponents.Add(new CategoryComponent { CategoryId = item, ComponentId = component1.Id });
                }
            }


           await _dbContext.Components.AddAsync(component1);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
