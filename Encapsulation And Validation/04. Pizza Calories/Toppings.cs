using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Pizza_Calories
{
    public class Toppings
    {
        private string typeOfTopping;
        private double weight;

        public string Type
        {
            set
            {
                if (value.ToLower() == "meat" || value.ToLower() == "veggies" || value.ToLower() == "cheese" || value.ToLower() == "sauce")
                {
                    this.typeOfTopping = value;
                }
                else
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }
        }

        public double Weight
        {
            set
            {
                if (value >= 1 && value <= 50)
                {
                    this.weight = value;
                }
                else
                    throw new ArgumentException($"{this.typeOfTopping} weight should be in the range [1..50].");
            }
        }

        public double Calories()
        {
            var modifier = 2.0;
            if (typeOfTopping.ToLower() == "meat")
                modifier *= 1.2;
            if (typeOfTopping.ToLower() == "veggies")
                modifier *= 0.8;
            if (typeOfTopping.ToLower() == "cheese")
                modifier *= 1.1;
            if (typeOfTopping.ToLower() == "sauce")
                modifier *= 0.9;

            var calories = this.weight * modifier;
            return calories;
        }
    }
}
