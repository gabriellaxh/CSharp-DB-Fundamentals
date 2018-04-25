using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student : Human
{
    private string facultyNumber;

    public Student(string firstName, string lastName, string facultyNumber)
        : base(firstName, lastName)
    {
        this.FacultyNumber = facultyNumber;
    }

    public string FacultyNumber
    {
        get
        {
            return this.facultyNumber;
        }
        set
        {
            if (value.Length < 5 || value.Length > 10 || value.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new ArgumentException("Invalid faculty number!");
            }
            this.facultyNumber = value;

        }
    }

    public override string ToString()
    {
        return $@"First Name: {FirstName}
               Last Name: {LastName}
               Faculty number: {facultyNumber}";
    }
}

