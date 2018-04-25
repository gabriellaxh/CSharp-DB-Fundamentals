using EmployeeFullInformation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace _08._Addresses_by_Town
{
    class Startup
    {
        static void Main(string[] args)
        {
            using(var db = new SoftUniContext())
            {
                var addresses = db.Addresses
                    .Include(a => a.Employees)
                    .OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .ToList();

                foreach (var addr in addresses)
                {
                    var townId = addr.TownId;
                    var town = db.Towns.Find(townId);

                    Console.WriteLine($"{addr.AddressText}, {town.Name} - {addr.Employees.Count} employees");
                }
            }
        }
    }
}
