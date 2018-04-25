using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Worker : Human
{
    private decimal weekSalary;
    private decimal workingHours;

    public Worker(string firstName, string lastName, decimal weekSalary, decimal workingHours)
        : base(firstName, lastName)
    {
        this.weekSalary = weekSalary;
        this.workingHours = workingHours;
    }

    public decimal WeekSalary
    {
        get
        {
            return this.weekSalary;
        }
        set
        {
            if (value > 10)
            {
                this.weekSalary = value;
            }
            else
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
        }
    }

    public decimal WorkingHours
    {
        get
        {
            return this.workingHours;
        }
        set
        {
            if (value >= 1 && value <= 12)
            {
                this.workingHours = value;
            }
            else
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
        }
    }

    private decimal Calc()
    {
        var perDay = WeekSalary / 5;
        var perHour = perDay / WorkingHours;

        return perHour;
    }

    public override string ToString()
    {
        return 
        $@"First Name: {FirstName}
        Last Name: {LastName}
        Week Salary: {weekSalary:f2}
        Hours per day: {workingHours:f2}
        Salary per hour: {Calc():f2}";
    }
}

