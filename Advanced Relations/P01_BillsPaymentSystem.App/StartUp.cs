namespace P01_BillsPaymentSystem.App
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;
    using System;
    using System.Globalization;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var userId = int.Parse(Console.ReadLine());
            try
            {
                using (var db = new BillsPaymentSystemContext())
                {
                    var user = db.Users
                        .Where(z => z.UserId == userId)
                        .Select(x => new
                        {
                            Name = $"{x.FirstName} {x.LastName}",

                            CreditCards = x.PaymentMethods.
                            Where(d => d.Type == PaymentMethodType.CreditCard)
                            .Select(h => h.CreditCard).ToList(),

                            BankAccounts = x.PaymentMethods
                            .Where(s => s.Type == PaymentMethodType.BankAccount)
                            .Select(y => y.BankAccount).ToList()

                        }).FirstOrDefault();

                    Console.WriteLine($"User: {user.Name}");

                    var bankAcc = user.BankAccounts;
                    if (bankAcc.Any())
                    {
                        Console.WriteLine("Bank Accounts:");
                        foreach (var ba in bankAcc)
                        {
                            Console.WriteLine($@"-- ID: {ba.BankAccountId}
-- Balance: {ba.balance:f2}
-- Bank: {ba.BankName}
-- SWIFT: {ba.SwiftCode}");
                        }
                    }

                    var creditCard = user.CreditCards;
                    if (creditCard.Any())
                    {
                        Console.WriteLine("Credit Cards:");
                        foreach (var cr in creditCard)
                        {
                            Console.WriteLine($@"-- ID: {cr.CreditCardId}
-- Limit: {cr.Limit:f2}
-- Money Owed: {cr.moneyOwed:f2}
-- Limit Left: {cr.LimitLeft}
-- Expiration Date: {cr.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"User with id {userId} not found!");
            }

           
        }
        
        private static void Seed(BillsPaymentSystemContext db)
        {
            using (db)
            {
                var user = new User()
                {
                    FirstName = "Simeon",
                    LastName = "Ivanov",
                    Email = "simoiv@abv.bg",
                    Password = "simosimoiv"

                };

                var creditCards = new CreditCard[]
                {
                    new CreditCard()
                    {
                        ExpirationDate = DateTime.ParseExact("20.05.2020", "dd.MM.yyyy", null),
                        Limit = 1000m,
                        moneyOwed = 5m
                    },
                    new CreditCard()
                    {
                        ExpirationDate = DateTime.ParseExact("30.04.2021", "dd.MM.yyyy", null),
                        Limit = 2000m,
                        moneyOwed = 100m
                    }
                };

                var bankAccount = new BankAccount()
                {
                    balance = 1500m,
                    BankName = "Swiss Bank",
                    SwiftCode = "SWSSBANK"
                };

                var paymentMehtods = new PaymentMethod[]
                {
                    new PaymentMethod()
                    {
                        User = user,
                        CreditCard = creditCards[0],
                        Type = PaymentMethodType.CreditCard
                    },

                    new PaymentMethod()
                    {
                        User = user,
                        CreditCard = creditCards[1],
                        Type = PaymentMethodType.CreditCard
                    },

                    new PaymentMethod()
                    {
                        User = user,
                        BankAccount = bankAccount,
                        Type = PaymentMethodType.BankAccount
                    }
                };

                db.Users.Add(user);
                db.CreditCards.AddRange(creditCards);
                db.BankAccounts.Add(bankAccount);
                db.PaymentMethods.AddRange(paymentMehtods);

                db.SaveChanges();
            }
        }
    }
}
