using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Models;
using Purple.ViewModels.Work;

namespace Purple.Areas.Admin.Controllers
{
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

        public async Task<IActionResult>  Create(WorkCreateViewModel component)
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




        }
    }
}
