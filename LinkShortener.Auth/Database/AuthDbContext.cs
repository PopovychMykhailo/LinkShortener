using LinkShortener.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Resource.Database
{
    public class AuthDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountEntity>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}
