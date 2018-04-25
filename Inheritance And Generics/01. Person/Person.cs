
using System;
using System.Linq;

public class Person
{
    private string name;
    public int age;

    public virtual string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            if (value.Count() >= 3)
            {
                this.name = value;
            }
            else
                throw new ArgumentException("Name's length should not be less than 3 symbols!");
        }
    }

    public virtual int Age
    {
        get
        {
            return this.age;
        }
        set
        {
            if (value >= 0)
            {
                this.age = value;
            }
            else
                throw new ArgumentException("Age must be positive!");
        }
    }

    public Person(string name, int age)
    {
        this.Name = name;
        this.age = age;
    }

    public override string ToString()
    {
        return $"Name: {this.Name}, Age: {this.Age}";
    }

}

