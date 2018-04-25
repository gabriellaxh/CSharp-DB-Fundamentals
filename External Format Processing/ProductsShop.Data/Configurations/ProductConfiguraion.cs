namespace ProductsShop.Data.Configurations
{
   using System;
    using System.Text;
    using ProductsShop.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ProductConfiguraion : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder.Property(x => x.Price)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.HasOne(x => x.Buyer)
                .WithMany(x => x.ProductsBought)
                .HasForeignKey(x => x.BuyerId);

            builder.HasOne(x => x.Seller)
                .WithMany(x => x.ProductsSold)
                .HasForeignKey(x => x.SellerId);
        }
    }
}
