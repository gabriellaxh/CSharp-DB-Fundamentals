using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data
{
    internal class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasAlternateKey(x => x.Name);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}