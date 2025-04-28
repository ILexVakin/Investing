using Investing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Investing.Data
{
    public class MainContext : DbContext
    {
         public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<StockMoexUnion> StockMoexUnion { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
            //builder.Entity<Stock>()
            //    .ToTable("stock").HasKey(k => k.Id);
            base.OnModelCreating(builder);

            builder.Entity<Credentials>()
          .HasOne(c => c.User);           // Credentials имеет одного User
          //.HasOne(u => u.Credentials)      // User может иметь много Credentials
          //.HasForeignKey(c => c.UserId)      // Внешний ключ в Credentials
          //.OnDelete(DeleteBehavior.Cascade); // Опционально: каскадное удаление
        }
    }
}
