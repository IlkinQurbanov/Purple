namespace Purple.Models
{
    public class FeaturedWorkPhoto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FeaturedWorkId { get; set; }
        public FeaturedWork FeaturedWork { get; set; }



    }
}
