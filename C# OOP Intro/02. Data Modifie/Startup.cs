using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    static void Main(string[] args)
    {
        var date1 = Console.ReadLine();
        var date2 = Console.ReadLine();

        var datesToCheck = new DateModifier();
        Console.WriteLine(datesToCheck.DaysDifference(date1,date2));
    }
}
