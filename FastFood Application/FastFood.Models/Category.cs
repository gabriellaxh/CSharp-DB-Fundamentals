namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
