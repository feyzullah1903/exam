using Circle.Models;
using Microsoft.EntityFrameworkCore;

namespace Circle.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) { }
       
        public DbSet<Memmbers> Memmbers { get; set; }

    }
}
