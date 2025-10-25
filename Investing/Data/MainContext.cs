using Investing.Models;
using Investing.Models.Redis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Investing.Data
{
    public class MainContext : DbContext
    {
         public MainContext(DbContextOptions<MainContext> options) : base(options)
        {}
        public DbSet<Stock> Stock { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<UserRole> UserRole { get; set; } = null!;
        public DbSet<Credentials>? Credentials { get; set; } = null!;
        public DbSet<StockMoexUnion> StockMoexUnion { get; set; } = null!;
        public DbSet<OriginalRedis> OriginalRedis { get; set; } = null!;
        public DbSet<DuplicateRedis> DuplicateRedis { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Credentials)
                .WithOne()
                .HasForeignKey<Credentials>(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.Role_id)
                .HasPrincipalKey(r => r.UserRoleId);
        }
    }
}
