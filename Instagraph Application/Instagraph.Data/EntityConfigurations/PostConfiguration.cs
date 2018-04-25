namespace Instagraph.Data
{
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Caption)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Picture)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}