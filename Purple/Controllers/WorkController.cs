using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.ViewModels;
using Purple.ViewModels.Work;

namespace Purple.Controllers
{
    public class WorkController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public WorkController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task<IActionResult> Index()
        {
          //  var components = await _appDbContext.Components.ToListAsync();
             var categories = await _appDbContext.Categories.Include(x => x.CategoryComponents)
                .ThenInclude(cc=>cc.Component).ToListAsync();


            var vm = new WorkIndexViewModels
            {
                categories = categories,
                //components = components
            };

            return View(vm);
        }

    }
}
