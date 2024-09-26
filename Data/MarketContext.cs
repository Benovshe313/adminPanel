
using admin_panel2.Models;
using Microsoft.EntityFrameworkCore;

namespace admin_panel2.Data
{
    internal class MarketContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-RFGQSC7;Initial Catalog=MarketApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bought> Boughts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category).
                HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Bought>().HasKey(b => b.Id);
        }
    }
}
        

