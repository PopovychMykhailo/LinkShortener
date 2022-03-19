using LinkShortener.Backend.Domain.Entities.Implimentations;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Backend.Database
{
    public class AppDbContext : DbContext
    {
        DbSet<LinkItem> Links { get; set; } 


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
