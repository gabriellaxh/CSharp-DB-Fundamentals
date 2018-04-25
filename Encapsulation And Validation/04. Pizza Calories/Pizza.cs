using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Pizza_Calories
{
    public class Pizza
    {
        public string name { get; set; }
        private Dough dough { get; set; }
        private List<Toppings> toppings { get; set; }

        public string Name
        {
            set
            {
                if (value.Count() > 1 && value.Count() < 15)
                {
                    this.Name = value;
                }
                else
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
        }
        public Dough Dough
        {
            set
            {
                this.dough = value;
            }
        }
        public List<Toppings> Toppings
        {
            set
            {
                if (value.Count <= 10)
                {
                    this.toppings = value;
                }
                else
                    throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
        }


        public double TotalCalories()
        {
            double toppingsCal = 0.0;
            foreach (var top in toppings)
            {
                toppingsCal += top.Calories();
            }
            var cal = toppingsCal + dough.Calories();
            return cal;
        }

        public Pizza(string name, Dough dough, List<Toppings> toppings)
        {
            this.name = name;
            this.dough = dough;
            this.Toppings = toppings;
        }
    }
}
