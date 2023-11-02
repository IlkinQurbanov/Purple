using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Purple.DAL;
using Purple.Models;
using Purple.ViewModels.Home;

namespace Purple.Controllers
{
    public class HomeController : Controller
    {
            private readonly AppDbContext _appDbContext;

            public HomeController(AppDbContext appDbContext)
            {
            _appDbContext = appDbContext;
            }
     
        public async Task<IActionResult> Index()
            { 

            var projectComponents = await _appDbContext.ProjectComponets.ToListAsync();


            var model = new HomeIndexViewModel
            {
                ProjectComponents = projectComponents
            };

            return View(model);
        }
    }
}
