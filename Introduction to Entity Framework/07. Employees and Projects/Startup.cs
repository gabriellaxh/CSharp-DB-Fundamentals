using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace _07._Employees_and_Projects
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniContext())
            {
                var employeesProject = db.Employees
                    .Include(e => e.EmployeesProjects)
                    .ThenInclude(p => p.Project)
                    .Where(e => e.EmployeesProjects.Any(s => s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .ToList();

                foreach (var employee in employeesProject)
                {
                    var managerId = employee.ManagerId;
                    var manger = db.Employees.Find(managerId);
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - Manager: {manger.FirstName} {manger.LastName}");

                    foreach (var proj in employee.EmployeesProjects)
                    {
                        var startDate = proj.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        var endDate = proj.Project.EndDate.ToString();

                        if (string.IsNullOrWhiteSpace(endDate))
                        {
                            endDate = "not finished";
                        }
                        else
                        {
                            endDate = proj.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        }

                        Console.WriteLine($"--{proj.Project.Name} - {startDate} - {endDate}");
                    }
                }
            }
        }
    }
}
