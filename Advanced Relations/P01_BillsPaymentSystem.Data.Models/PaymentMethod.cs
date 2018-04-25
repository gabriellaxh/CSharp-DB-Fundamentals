﻿namespace P01_BillsPaymentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        public PaymentMethodType Type { get; set; }

        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }

        public int  PaymentType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
