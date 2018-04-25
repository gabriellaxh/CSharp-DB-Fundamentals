namespace Instagraph.Data.Configurations
{
    using Instagraph.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            //how to make it unique
            builder.HasAlternateKey(x => x.Username);

            builder.Property(x => x.Username)
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(x => x.ProfilePicture)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
