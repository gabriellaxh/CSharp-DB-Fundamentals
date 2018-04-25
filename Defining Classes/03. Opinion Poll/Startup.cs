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

        var peopleList = new List<Person>();

        for (int i = 0; i < num; i++)
        {
            var info = Console.ReadLine().Split().ToList();

            string name = info[0];
            int age = int.Parse(info[1]);
            var person = new Person();
            person.Name = name;
            person.Age = age;

            peopleList.Add(person);
        }

        var result = peopleList
            .Where(a => a.Age > 30)
            .OrderBy(n => n.Name)
            .ToList();

        foreach (var person in result)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }

    }
}

