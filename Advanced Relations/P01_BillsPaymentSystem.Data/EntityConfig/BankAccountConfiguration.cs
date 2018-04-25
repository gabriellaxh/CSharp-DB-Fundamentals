namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> bankAccount)
        {
            bankAccount.HasKey(b => b.BankAccountId);

            bankAccount.Property(p => p.balance)
                .IsRequired()
                .HasDefaultValue(0);

            bankAccount.Property(b => b.BankName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            bankAccount.Property(b => b.SwiftCode)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            bankAccount.Ignore(b => b.PaymentMethodId);
        }
    }
}
