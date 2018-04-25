using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _14._Delete_Project_by_Id
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new SoftUniContext())
            {
                var projectToDel = db.EmployeesProjects
                    .Include(p => p.Project)
                    .Where(i => i.ProjectId == 2)
                    .ToList();

                foreach (var proj in projectToDel)
                {
                    db.EmployeesProjects.Remove(proj);
                    db.SaveChanges();
                }

                var project = db.Projects.Find(2);
                db.Projects.Remove(project);
                db.SaveChanges();

                var projToPrint = db.Projects
                    .Select(e => new
                    {
                        e.Name
                    })
                    .Take(10)
                    .ToList();

                foreach (var pr in projToPrint)
                {
                    Console.WriteLine($"{pr.Name}");
                }
            }
        }
    }
}
