using LinkShortener.Resource.Domain.Entities.Implimentations;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Resource.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<LinkItem> Links { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
