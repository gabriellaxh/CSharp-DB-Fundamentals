namespace ProductsShop.Data.Configurations
{
    using System;
    using System.Text;
    using ProductsShop.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.Age)
                .IsRequired(false)
                .HasColumnType("INT");
        }
    }
}
