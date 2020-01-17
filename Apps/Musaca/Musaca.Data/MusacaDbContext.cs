namespace Musaca.Data
{
    using Microsoft.EntityFrameworkCore;
    using Musaca.Data.Models;

    public class MusacaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithOne()
                .HasForeignKey(x => x.Id);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Cashier);

            base.OnModelCreating(modelBuilder);
        }
    }
}