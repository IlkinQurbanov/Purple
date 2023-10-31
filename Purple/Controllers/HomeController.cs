using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Purple.Models;
using Purple.ViewModels.Home;

namespace Purple.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var projectComponents = new List<ProjectComponent>
            {
                new ProjectComponent { Id=1, Title="UI/UX Design",Description="Our works", ImagePath="/assets/img/services-01.jpg" },
                new ProjectComponent { Id = 2, Title = "Web Design", Description = "Our works", ImagePath= "/assets/img/services-01.jpg" },
                new ProjectComponent { Id = 3, Title = "Mobil Development", Description = "Our works", ImagePath= "/assets/img/services-01.jpg" },
              new ProjectComponent { Id = 2, Title = "Marketing", Description = "Our works", ImagePath= "/assets/img/services-01.jpg" }

                };

            var model = new HomeIndexViewModel
            {
                ProjectComponents = projectComponents
            };

            return View(model);
        }
    }
}
