using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Schools.Models
{
    public class SchoolDbContext:IdentityDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options):base(options) { }
        
        public DbSet<ApplicationUsers> SchoolUsers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Lessons> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUsers>().ToTable(nameof(SchoolUsers));
            builder.Entity<Students>().ToTable(nameof(Students));
            builder.Entity<Teachers>().ToTable(nameof(Teachers));
            builder.Entity<IdentityUser>().ToTable(nameof(Users));
            builder.Entity<IdentityRole>().ToTable(nameof(Roles));
            builder.Entity<Lessons>().ToTable(nameof(Lessons));
        }
    }
}
