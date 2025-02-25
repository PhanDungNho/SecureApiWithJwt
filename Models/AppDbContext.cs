using Microsoft.EntityFrameworkCore;

namespace SecureApiWithJwt.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Intern> Interns { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<AllowAccess> AllowAccesses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<AllowAccess>()
                .HasOne(a => a.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleId);

        }
    }
}
