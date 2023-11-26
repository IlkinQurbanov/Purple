using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;

namespace Purple.ViewComponents
{
    public class ObjectiveComponentViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public ObjectiveComponentViewComponent(AppDbContext appDbContext)

        {
            this._appDbContext = appDbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _appDbContext.ObjectiveComponents.ToListAsync();
            return View(model);
        }
    }
}
