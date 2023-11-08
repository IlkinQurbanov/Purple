using System.ComponentModel.DataAnnotations;

namespace Purple.Models
{
    public class RecentWork
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Title must be written"), MinLength(3,ErrorMessage="Min Length words must be 3")]
        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImagePath { get; set; }
    }
}
