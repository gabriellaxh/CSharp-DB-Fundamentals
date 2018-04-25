using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data
{
    internal class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(x => new
            {
                x.UserId,
                x.FollowerId
            });

            builder.HasOne(x => x.User)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Follower)
                .WithMany(x => x.UsersFollowing)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}