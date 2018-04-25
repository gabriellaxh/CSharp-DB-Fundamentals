using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _10._Departments_with_More_Than_5_Employees
{
    class Startup
    {
        static void Main(string[] args)
        {
            using(var db = new SoftUniContext())
            {
                var departments = db.Departments
                    .Include(e => e.Employees)
                    .Where(e => e.Employees.Count > 5)
                    .OrderBy(e => e.Employees.Count)
                    .ThenBy(d => d.Name)
                    .ToList();

                foreach (var dep in departments)
                {
                    var manager = dep.Manager;
                    Console.WriteLine("----------");
                    Console.WriteLine($"{dep.Name} - {manager.FirstName} {manager.LastName}");

                    foreach (var emp in dep.Employees.OrderBy(f => f.FirstName).ThenBy(l => l.LastName))
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                    }
                }
            }
        }
    }
}
