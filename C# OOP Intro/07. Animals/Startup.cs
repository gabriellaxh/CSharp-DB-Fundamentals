using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Startup
{
    static void Main(string[] args)
    {
        var animals = new List<Animal>();
        string command;

        while ((command = Console.ReadLine()) != "Beast!")
        {
            var info = Console.ReadLine().Split();
            var name = info[0];
            var age = int.Parse(info[1]);
            var gender = info[2];
            try
            {
                switch (command)
                {
                    case "Cat":
                        animals.Add(new Cat(name, age, gender));
                        break;
                    case "Dog":
                        animals.Add(new Dog(name, age, gender));
                        break;
                    case "Frog":
                        animals.Add(new Frog(name, age, gender));
                        break;
                    case "Kitten":
                        animals.Add(new Kitten(name, age, gender));
                        break;
                    case "Tomcat":
                        animals.Add(new Tomcat(name, age, gender));
                        break;
                    default:
                        throw new ArgumentException("Invalid input!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        foreach (var animal in animals)
        {
            Console.WriteLine(animal);
        }
    }
}

