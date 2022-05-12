using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext([NotNullAttribute] DbContextOptions options) : base(options)
        { }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new BasketMap(builder.Entity<Basket>());
            new ItemMap(builder.Entity<Item>());
        }
    }
}