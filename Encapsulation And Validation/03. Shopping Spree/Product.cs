using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Shopping_Spree
{
   public class Product
    {
        private string name { get; set; }
        private double price { get; set; }


        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }

        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Price cannot be zero or negative");
                }
                this.price = value;
            }
        }
    }
}
