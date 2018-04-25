namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> paymentMethod)
        {
            paymentMethod.HasKey(e => e.Id);

            paymentMethod.HasIndex(i => new
            {
                i.UserId,
                i.BankAccountId,
                i.CreditCardId
            })
            .IsUnique();

            paymentMethod.HasOne(u => u.User)
                 .WithMany(p => p.PaymentMethods)
                 .HasForeignKey(d => d.UserId);

            paymentMethod.HasOne(b => b.BankAccount)
                .WithOne(b => b.PaymentMethod)
                .HasForeignKey<PaymentMethod>(b => b.BankAccountId);

            paymentMethod.HasOne(s => s.CreditCard)
                .WithOne(a => a.PaymentMethod)
                .HasForeignKey<PaymentMethod>(z => z.CreditCardId); 
        }
    }
}
