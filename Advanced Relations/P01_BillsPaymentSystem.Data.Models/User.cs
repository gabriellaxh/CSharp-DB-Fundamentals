namespace P01_BillsPaymentSystem.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.PaymentMethods = new List<PaymentMethod>();
        }
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
