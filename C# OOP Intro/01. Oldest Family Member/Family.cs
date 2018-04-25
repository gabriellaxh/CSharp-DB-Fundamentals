using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Family
{
    private List<Person> members;

    public Family()
    {
        this.members = new List<Person>();
    }

    public  void AddMember(Person member)
    {
        members.Add(member);
    }

    public string GetOldestMember()
    {
        var oldest = members.OrderByDescending(x => x.Age).First();
        return $"{oldest.Name} {oldest.Age}";
    }
}

