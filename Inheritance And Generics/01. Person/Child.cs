
using System;

public class Child : Person
{
    public Child(string name, int age)
            : base(name, age)
    {
        this.Age = age;
    }

    public new int Age
    {
        get
        {
            return base.age;
        }
        set
        {
            if (value < 15)
            {
                base.Age = value;
            }
            else
                throw new ArgumentException("Child's age must be less than 15!");

        }
    }
}

