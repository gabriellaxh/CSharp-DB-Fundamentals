namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(a => a.UserId);

            user.Property(z => z.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(80);

            user.Property(a => a.FirstName)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(50);

            user.Property(h => h.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            user.Property(p => p.Password)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(25);
        }
    }
}
