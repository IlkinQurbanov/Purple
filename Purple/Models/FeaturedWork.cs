namespace Purple.Models
{
    public class FeaturedWork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FeaturedWorkPhoto> FeaturedWorkPhotos { get; set; }

    }
}
