namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> creditCard)
        {
            creditCard.HasKey(c => c.CreditCardId);

            creditCard.Property(c => c.Limit)
                .IsRequired();

            creditCard.Property(c => c.moneyOwed)
                .IsRequired()
                .HasColumnType("DECIMAL");

            creditCard.Ignore(c => c.PaymentMethodId);
            creditCard.Ignore(c => c.LimitLeft);

            creditCard.Property(c => c.ExpirationDate)
                .IsRequired()
                .HasColumnType("DATETIME2");
        }
    }
}
