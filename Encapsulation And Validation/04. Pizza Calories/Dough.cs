using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Pizza_Calories
{
    public class Dough
    {
        private string flourType;
        private string bakingTechnique;
        private double weight;

        public string Flour
        {
            set
            {
                if (value.ToLower() == "white" || value.ToLower() == "wholegrain")
                {
                    this.flourType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
            }
        }

        public string Baking
        {
            set
            {
                if (value.ToLower() == "crispy" || value.ToLower() == "chewy" || value.ToLower() == "homemade")
                {
                    this.bakingTechnique = value;
                }
                else
                {
                    throw new ArgumentException("Invalid baking technique");
                }
            }
        }

        public double Weight
        {
            set
            {
                if (value >= 1 && value <= 200)
                {
                    this.weight = value;
                }
                else
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
        }

        public double Calories()
        {
            var modifier = 2.0;
            if (this.flourType.ToLower() == "white")
            {
                modifier *= 1.5;
                if (this.bakingTechnique.ToLower() == "cispy")
                    modifier *= 0.9;
                if (this.bakingTechnique.ToLower() == "chewy")
                    modifier *= 1.1;
                if (this.bakingTechnique.ToLower() == "homemade")
                    modifier *= 1.0;
            }
            else
            {
                modifier *= 1.5;
                if (this.bakingTechnique.ToLower() == "cispy")
                    modifier *= 0.9;
                if (this.bakingTechnique.ToLower() == "chewy")
                    modifier *= 1.1;
                if (this.bakingTechnique.ToLower() == "homemade")
                    modifier *= 1.0;
            }

            double calories = this.weight * modifier;
            return calories;
        }
    }
}
