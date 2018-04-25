namespace ProductsShop.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using ProductsShop.Data.Configurations;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
        {  }

        public ProductsShopContext(DbContextOptions options)
            :base(options)
        {  }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryProduct> CategoryProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguraion());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryProductConfiguration());
        }
    }
}
