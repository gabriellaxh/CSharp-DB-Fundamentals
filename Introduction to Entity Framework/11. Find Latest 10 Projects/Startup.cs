using EmployeeFullInformation;
using System;
using System.Globalization;
using System.Linq;

namespace _11._Find_Latest_10_Projects
{
    class Startup
    {
        static void Main(string[] args)
        {
            using(var db = new SoftUniContext())
            {
                var projects = db.Projects
                    .OrderByDescending(s => s.StartDate)
                    .Take(10)
                    .OrderBy(n => n.Name)
                    .ToList();

                foreach (var pr in projects)
                {
                    Console.WriteLine($"{pr.Name}");
                    Console.WriteLine($"{pr.Description}");
                    Console.WriteLine($"{pr.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture)}");
                }
                    
            }
        }
    }
}
