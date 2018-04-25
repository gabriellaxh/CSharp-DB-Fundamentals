namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        public decimal balance;
        public string BankName { get; set; }
        public string SwiftCode { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public decimal Balance
        {
            get
            {
                return this.balance;
            }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Balance should be not negative.");
                }
                this.balance = value;
            }
        }

        public void WithDraw(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            if(amount > this.balance)
            {
                throw new ArgumentException("Insufficient funds!");
            }
            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            this.Balance += amount;
        }
    }
}
