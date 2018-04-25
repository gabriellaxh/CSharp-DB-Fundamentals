using Microsoft.EntityFrameworkCore;
using System;

namespace P03_SalesDatabase
{
    using P03_SalesDatabase.Data;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public SalesContext()
        { }

        public SalesContext(DbContextOptions options)
            :base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

                entity.Property(e => e.Quantity)
                .IsUnicode(true)
                .HasMaxLength(250);

                entity.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("DECIMAL");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasMaxLength(80);

                entity.Property(e => e.CreditCardNumber)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(80);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(p => p.Product)
                .WithMany(s => s.Sales)
                .HasForeignKey(p => p.ProductId);

                entity.HasOne(c => c.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.CustomerId);

                entity.HasOne(s => s.Store)
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.StoreId);
            });
        }
    }
}
