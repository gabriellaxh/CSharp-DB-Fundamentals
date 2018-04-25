using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Customer)
                .IsRequired();

            builder.Property(x => x.DateTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.TotalPrice)
                .IsRequired();
        }
    }
}