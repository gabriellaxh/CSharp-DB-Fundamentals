using EmployeeFullInformation;
using System;
using System.Linq;

namespace _13._Find_Employees_by_First_Name_Starting_With_Sa
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(f => f.FirstName.StartsWith("Sa"))
                    .OrderBy(f => f.FirstName)
                    .ThenBy(l => l.LastName)
                    .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
                }
            }
        }
    }
}
