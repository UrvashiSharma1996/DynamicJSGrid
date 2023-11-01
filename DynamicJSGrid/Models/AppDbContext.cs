using Microsoft.EntityFrameworkCore;

namespace DynamicJSGrid.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //Constructor 
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
