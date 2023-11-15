using Microsoft.AspNetCore.Mvc.Rendering;

namespace Purple.ViewModels.Work
{
    public class WorkCreateViewModel
    {

        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImagePath { get; set; }
        public List<SelectListItem> Items { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
