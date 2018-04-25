using EmployeeFullInformation;
using System;
using System.Linq;

namespace _05._Employees_from_Research_and_Development
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(d => d.Department.Name == "Research and Development")
                    .OrderBy(s => s.Salary)
                    .ThenByDescending(f => f.FirstName)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Department.Name,
                        e.Salary
                    });

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.Name} - ${employee.Salary:f2}");

                }
            }
        }
    }
}
