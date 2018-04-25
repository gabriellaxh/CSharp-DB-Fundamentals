namespace ProductsShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using ProductsShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(x => new
            {
                x.ProductId,
                x.CategoryId
            });

           
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
