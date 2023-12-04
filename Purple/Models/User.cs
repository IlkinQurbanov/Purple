using Microsoft.AspNetCore.Identity;

namespace Purple.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }

    }
}
