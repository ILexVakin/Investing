﻿using Investing.Models;
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
        public DbSet<Credentials>? Credentials { get; set; } = null!;
        public DbSet<StockMoexUnion> StockMoexUnion { get; set; } = null!;

    }
}
