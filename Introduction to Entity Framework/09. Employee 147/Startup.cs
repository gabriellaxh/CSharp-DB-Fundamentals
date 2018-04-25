using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _09._Employee_147
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employee = db.Employees
                    .Include(p => p.EmployeesProjects)
                    .ThenInclude(p => p.Project)
                    .Where(id => id.EmployeeId == 147)
                    .ToList();

                foreach (var emp in employee)
                {
                    var jobTitle = emp.JobTitle;
                    System.Console.WriteLine($"{emp.FirstName} {emp.LastName} - {jobTitle}");

                    foreach (var proj in emp.EmployeesProjects.OrderBy(n => n.Project.Name))
                    {
                        System.Console.WriteLine($"{proj.Project.Name}");
                    }
                }

            }
        }
    }
}
