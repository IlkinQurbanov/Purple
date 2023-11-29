using Purple.Models;

namespace Purple.ViewModels.FeaturedWorkComp
{
    public class FeaturedWorkUpdateViewModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<FeaturedWorkPhoto>? FeaturedWorkPhotos { get; set; }

    }
}
