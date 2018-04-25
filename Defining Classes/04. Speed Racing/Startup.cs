using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Startup
{
    static void Main(string[] args)
    {
        int num = int.Parse(Console.ReadLine());
        var carList = new List<Car>();

        for (int i = 0; i < num; i++)
        {
            var info = Console.ReadLine().Split().ToList();

            string model = info[0];
            double fuelAmount = double.Parse(info[1]);
            double fuelConsump = double.Parse(info[2]);

            var car = new Car();
            car.Model = model;
            car.FuelAmount = fuelAmount;
            car.FuelConsumpPerKm = fuelConsump;

            carList.Add(car);
        }

        var driving = Console.ReadLine().Split().ToList();
        while (driving[0] != "End")
        {
            Calc(driving[1], double.Parse(driving[2]), carList);
            driving = Console.ReadLine().Split().ToList();
        }

        foreach (var car in carList)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.DistanceTraveled}");
        }
    }

    public static void Calc(string model, double distance, List<Car> carList)
    {
        int index = carList.FindIndex(a => a.Model == model);
        if (carList[index].FuelAmount >= carList[index].FuelConsumpPerKm * distance)
        {
            carList[index].FuelAmount -= carList[index].FuelConsumpPerKm * distance;
            carList[index].DistanceTraveled += distance;
        }
        else
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
    }
}

