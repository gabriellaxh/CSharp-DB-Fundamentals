using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Startup
{
    static void Main(string[] args)
    {
        int num = int.Parse(Console.ReadLine());
        var employeeDict = new Dictionary<string, List<Employee>>();

        for (int i = 0; i < num; i++)
        {
            var info = Console.ReadLine().Split();
            string name = info[0];
            double salary = double.Parse(info[1]);
            string position = info[2];
            string department = info[3];

            var employee = new Employee(name, salary, position, department);

            if(info.Length == 5)
            {
                bool isInt = int.TryParse(info[4], out int age);
                if(isInt == true)
                {
                    employee.Age = age;
                }
                else
                {
                    employee.Email = info[4];
                }
            }

            if(info.Length == 6)
            {
                employee.Email = info[4];
                employee.Age = int.Parse(info[5]);
            }

            if (!employeeDict.ContainsKey(department))
            {
                employeeDict.Add(department, new List<Employee>());
            }
            employeeDict[department].Add(employee);
        }

        double highestAvgSalary = 0;
        string departmentWithHighestAvgSal = "none";
        foreach (var dep in employeeDict)
        {
            double totalSalary = 0.0;
            foreach (var emp in dep.Value)
            {
                totalSalary += emp.Salary;
            }

            double avgSalary = totalSalary / dep.Value.Count();

            if(avgSalary > highestAvgSalary)
            {
                highestAvgSalary = avgSalary;

                departmentWithHighestAvgSal = dep.Key;
            }
        }

        var result = employeeDict[departmentWithHighestAvgSal].OrderByDescending(a => a.Salary).ToList();
        Console.WriteLine($"Highest Average Salary: {departmentWithHighestAvgSal}");
        foreach (var employee in result)
        {
            Console.WriteLine($"{employee.Name} {employee.Salary:f2} {employee.Email} {employee.Age}");
        }

    }
}

