using Microsoft.EntityFrameworkCore;

namespace DatingApp.api.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<value> values { get; set; }
    }
}