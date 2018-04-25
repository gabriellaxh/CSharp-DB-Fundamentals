using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data
{
    internal class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path)
               .IsRequired();

            builder.Property(x => x.Size)
               .IsRequired();
        }
    }
}