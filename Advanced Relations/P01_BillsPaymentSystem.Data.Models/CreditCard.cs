namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        public DateTime expirationDate;
        public decimal limit;
        public decimal moneyOwed;

        public decimal LimitLeft => limit - moneyOwed;
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public CreditCard()
        { }

        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
            this.ExpirationDate = expirationDate;
        }

        public decimal Limit
        {
            get
            {
                return this.limit;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Limit should be not negative.");
                }
                this.limit = value;
            }
        }

        public decimal MoneyOwed
        {
            get
            {
                return this.moneyOwed;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money owed by you should be not negative.");
                }
                if (value > this.Limit)
                {
                    throw new ArgumentException("Money owed by you should be less than our Limit.");
                }
                this.moneyOwed = value;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return this.expirationDate;
            }
            set
            {
                DateTime dateNow = DateTime.Now;

                if (dateNow > value)
                {
                    throw new ArgumentException("Credit card expiration date should be latter date than today.");
                }
                this.expirationDate = value;
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            if (amount > this.LimitLeft)
            {
                throw new ArgumentException("Insufficient funds!");
            }
            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }

            if(amount <= this.MoneyOwed)
            {
                this.MoneyOwed += amount;
            }
            else
            {
                this.Limit = amount - this.MoneyOwed;
                this.MoneyOwed = 0;
            }
        }
    }
}
