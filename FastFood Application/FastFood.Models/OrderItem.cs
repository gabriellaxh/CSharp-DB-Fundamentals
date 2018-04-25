namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        public int OrderId { get; set; }
        [Required]
        public Order Order { get; set; }


        public int ItemId { get; set; }
        [Required]
        public Item Item { get; set; }

        [Required]
        [Range(typeof(int), "1", "79228162514264337593543950335")]
        public int Quantity { get; set; }
    }
}