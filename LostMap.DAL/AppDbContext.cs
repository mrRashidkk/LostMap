using LostMap.DAL.InitData;
using LostMap.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LostMap.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Loss> Losses { get; set; }
        public DbSet<Finding> Findings { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>().HasData(RoleData.Roles);
            modelBuilder.Entity<Status>().HasData(StatusData.Statuses);

            base.OnModelCreating(modelBuilder);
        }
    }
}