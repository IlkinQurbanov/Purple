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

    }
}
