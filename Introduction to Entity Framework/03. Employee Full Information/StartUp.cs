using System;
using System.Linq;

namespace EmployeeFullInformation
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
               .Select(e => new
               {
                   e.FirstName,
                   e.LastName,
                   e.MiddleName,
                   e.JobTitle,
                   e.Salary
               })
               .ToList();

                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
                }
            }

        }
    }
}
