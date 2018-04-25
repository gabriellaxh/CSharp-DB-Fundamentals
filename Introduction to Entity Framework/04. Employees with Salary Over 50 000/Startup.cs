using EmployeeFullInformation;
using System;
using System.Linq;

namespace _04._Employees_with_Salary_Over_50_000
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(s => s.Salary > 50000)
                    .OrderBy(n => n.FirstName)
                    .Select(f => f.FirstName);

                foreach (var emp in employees)
                {
                    Console.WriteLine(emp);
                }
            }
        }
    }
}