using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Shopping_Spree
{
    class Startup
    {
        static void Main(string[] args)
        {
            try
            {
                var personInfo = Console.ReadLine().Split(';').ToList();
                var listOfPeople = new List<Person>();
                for (int i = 0; i < personInfo.Count; i++)
                {
                    var infoPers = personInfo[i].Split('=').ToList();
                    string namePers = infoPers[0];
                    double moneyPers = double.Parse(infoPers[1]);

                    var person = new Person();
                    person.Name = namePers;
                    person.Money = moneyPers;
                    person.Bag = new List<string>();
                    listOfPeople.Add(person);
                }


                var productInfo = Console.ReadLine().Split(';').ToList();
                var listOfProducts = new List<Product>();
                for (int i = 0; i < productInfo.Count; i++)
                {
                    var infoProd = productInfo[i].Split('=').ToList();
                    var nameProd = infoProd[0];
                    var priceProd = double.Parse(infoProd[1]);

                    var product = new Product();
                    product.Name = nameProd;
                    product.Price = priceProd;
                    listOfProducts.Add(product);


                }

                var command = Console.ReadLine();
                while (command != "END")
                {
                    var com = command.Split();
                    var pers = com[0];
                    var prod = com[1];

                    int personIndex = listOfPeople.FindIndex(a => a.Name == pers);
                    int productIndex = listOfProducts.FindIndex(b => b.Name == prod);

                    if (listOfPeople[personIndex].Money >= listOfProducts[productIndex].Price)
                    {
                        listOfPeople[personIndex].Money -= listOfProducts[productIndex].Price;
                        listOfPeople[personIndex].Bag.Add(prod);
                        Console.WriteLine($"{pers} bought {prod}");
                    }
                    else
                        Console.WriteLine($"{pers} can't afford {prod}");


                    command = Console.ReadLine();
                }

                foreach (var pers in listOfPeople)
                {
                    var bagOfProducts = string.Join(", ", pers.Bag);
                    if (string.IsNullOrEmpty(bagOfProducts))
                    {
                        Console.WriteLine($"{pers.Name} - Nothing bought");
                        break;
                    }
                    Console.WriteLine($"{pers.Name} - {bagOfProducts}");

                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }



        }
    }
}
