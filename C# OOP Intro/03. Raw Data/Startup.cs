using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    static void Main(string[] args)
    {
        var number = int.Parse(Console.ReadLine());
        var carsList = new List<Car>();
        for (int i = 0; i < number; i++)
        {
            var info = Console.ReadLine().Split();

            var model = info[0];
            var engine = new Engine(int.Parse(info[1]), int.Parse(info[2]));
            var cargo = new Cargo(int.Parse(info[3]), info[4]);
            var tire = new Tire(
                                double.Parse(info[5]),
                                int.Parse(info[6]),
                                double.Parse(info[7]),
                                int.Parse(info[8]),
                                double.Parse(info[9]),
                                int.Parse(info[10]),
                                double.Parse(info[11]),
                                int.Parse(info[12]));
            var car = new Car(model, engine, cargo, tire);
            carsList.Add(car);
        }
        var type = Console.ReadLine();

        if(type == "fragile")
        {
            foreach (var car in carsList.Where(x => x.Cargo.CargoType == "fragile"
            && (x.Tire.Tire1Pressure < 1 || x.Tire.Tire2Pressure < 1 || x.Tire.Tire3Pressure < 1 || x.Tire.Tire4Pressure < 1)))
            {
                Console.WriteLine(car.Model);  
            }
        }
        else if(type == "flammable")
        {
            foreach (var car in carsList.Where(x => x.Cargo.CargoType == "flammable"
            && x.Engine.EnginePower > 250))
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}

