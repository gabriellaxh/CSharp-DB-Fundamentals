using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _12._Increase_Salaries
{
    class Startup
    {
        static void Main(string[] args)
        {
           using(var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Include(d => d.Department)
                    .Where(d => d.Department.Name == "Engineering"
                    || d.Department.Name == "Tool Design"
                    || d.Department.Name == "Marketing"
                    || d.Department.Name == "Information Services")
                    .OrderBy(f => f.FirstName)
                    .ThenBy(l => l.LastName)
                    .ToList();

                foreach (var employee in employees)
                {
                    employee.Salary *= 1.12m;
                    db.SaveChanges();
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
                }
            } 
        }
    }
}
