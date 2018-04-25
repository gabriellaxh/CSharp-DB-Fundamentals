using EmployeeFullInformation;
using System;
using System.Linq;

namespace _06._Adding_a_New_Address_and_Updating_Employee
{
    class Startup
    {
        static void Main(string[] args)
        {
            var db = new SoftUniContext();

            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            db.Addresses.Add(address);

            var employee = db.Employees
                .Where(n => n.LastName == "Nakov")
                .FirstOrDefault();

            employee.Address = address;
            db.SaveChanges();

            var emp = db.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(a => new
                {
                    a.Address.AddressText
                });

            foreach (var e in emp)
            {
                Console.WriteLine(e.AddressText);
            }
        }
    }
}
