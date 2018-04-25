using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Pizza_Calories
{
    public class Startup
    {
        static void Main(string[] args)
        {
            try
            {
                var info = Console.ReadLine();
                string name = "";
                var dough = new Dough();
                var toppings = new List<Toppings>();
                while (info != "END")
                {
                    var text = info.Split().ToList();


                    if (text[0] == "Pizza")
                    {
                        name = text[1];
                    }


                    else if (text[0] == "Dough")
                    {
                        dough.Flour = text[1];
                        dough.Baking = text[2];
                        dough.Weight = double.Parse(text[3]);
                    }


                    else
                    {
                        var topping = new Toppings();
                        topping.Type = text[1];
                        topping.Weight = double.Parse(text[2]);
                        toppings.Add(topping);
                    }

                    info = Console.ReadLine();
                }

                var pizza = new Pizza(name, dough, toppings);
                var totalCal = pizza.TotalCalories();
                Console.WriteLine($"{pizza.name} - {totalCal:f2} Calories.");
            }

            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            
            
            

        }
    }
}
