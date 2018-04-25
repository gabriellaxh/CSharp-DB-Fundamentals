using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Shopping_Spree
{
    class Person
    {

        private string name { get; set; }
        private double money { get; set; }
        public List<string> Bag { get; set; }

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

        public double Money
        {
            get
            {
                return this.money;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }
    }
}
