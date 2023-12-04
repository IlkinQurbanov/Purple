
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Purple.Models;

namespace Purple.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {

        }
        public DbSet<ProjectComponent> ProjectComponets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<RecentWork> RecentWorks { get; set; }
        public DbSet<CategoryComponent> CategoryComponents { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet <ObjectiveComponent> ObjectiveComponents { get; set; }
        public DbSet <FeaturedWork> FeaturedWork { get; set; }
        public DbSet <FeaturedWorkPhoto> FeaturedWorkPhotos { get; set; }




    }
}
