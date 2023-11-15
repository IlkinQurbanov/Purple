using Microsoft.EntityFrameworkCore;
using Purple.Models;

namespace Purple.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {

        }
        public DbSet<ProjectComponent> ProjectComponets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<RecentWork> RecentWorks { get; set; }
        public DbSet<CategoryComponent> CategoryComponents { get; set; }



    }
}
