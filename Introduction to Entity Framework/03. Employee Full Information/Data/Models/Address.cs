using System;
using System.Collections.Generic;

namespace EmployeeFullInformation
{
    public partial class Address
    {
        public Address()
        {
            Employees = new HashSet<Employees>();
        }

        public int AddressId { get; set; }
        public string AddressText { get; set; }
        public int? TownId { get; set; }

        public Town Town { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
