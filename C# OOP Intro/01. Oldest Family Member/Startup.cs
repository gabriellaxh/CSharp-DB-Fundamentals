using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class Startup
{
    public static void Main(string[] args)
    {
        MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
        MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
        if (oldestMemberMethod == null || addMemberMethod == null)
        {
            throw new Exception();
        }

        var n = int.Parse(Console.ReadLine());
        var family = new Family();

        for (int i = 0; i < n; i++)
        {
            var info = Console.ReadLine().Split();
            var member = new Person(info[0], decimal.Parse(info[1]));
            family.AddMember(member);
        }

        Console.WriteLine(family.GetOldestMember());
    }
}

